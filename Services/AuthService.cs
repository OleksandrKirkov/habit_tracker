using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Configuration;
using Core.DTO.Auth;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Services;

public interface IAuthService
{
    Task<TokenResponse> RegisterAsync(RegisterRequest request);
    Task<TokenResponse> LoginAsync(LoginRequest request);
    Task<TokenResponse> RefreshAsync();
    Task LogoutAsync();
    Task<MeResponse> GetMeAsync(ClaimsPrincipal userClaims);
    Task<TokenResponse> GenerateTokensAsync(User user);
}

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uow;
    private readonly AuthOptions _auth;
    private readonly IHttpContextAccessor _httpContext;

    public AuthService(IUnitOfWork uow, IOptions<AuthOptions> authOptions, IHttpContextAccessor httpContext)
    {
        _uow = uow;
        _auth = authOptions.Value;
        _httpContext = httpContext;
    }

    public async Task<TokenResponse> RegisterAsync(RegisterRequest request)
    {
        if ((await _uow.Users.FindAsync(u => u.Email == request.Email)).Any())
            throw new Exception("Email already exists");

        var user = new User
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Name = request.Name ?? "User",
        };

        await _uow.Users.AddAsync(user);
        await _uow.CompleteAsync();
        return await GenerateTokensAsync(user);
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        var user = await _uow.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var oldTokens = await _uow.RefreshTokens.FindAsync(x => x.UserId == user.Id && !x.isRevoked);

        foreach (var token in oldTokens)
            token.isRevoked = true;

        await _uow.CompleteAsync();

        return await GenerateTokensAsync(user);
    }

    public async Task<TokenResponse> RefreshAsync()
    {
        var token = _httpContext.HttpContext?.Request.Cookies["refresh_token"];
        if (string.IsNullOrWhiteSpace(token)) throw new Exception("Missing refresh token");

        var stored = await _uow.RefreshTokens.GetWithUserByTokenAsync(token);

        if (stored == null || stored.ExpiresAt < DateTime.UtcNow)
            throw new Exception("Invalid refresh token");

        stored.isRevoked = true;
        await _uow.CompleteAsync();

        return await GenerateTokensAsync(stored.User);
    }

    public async Task LogoutAsync()
    {
        var token = _httpContext.HttpContext?.Request.Cookies["refresh_token"];
        if (string.IsNullOrWhiteSpace(token)) return;

        var stored = await _uow.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token);
        if (stored != null)
        {
            stored.isRevoked = true;
            await _uow.CompleteAsync();
        }

        _httpContext.HttpContext?.Response.Cookies.Delete("refresh_token");
    }

    public async Task<MeResponse> GetMeAsync(ClaimsPrincipal userClaims)
    {
        var idClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idClaim == null) throw new Exception("invalid access token");

        var id = int.Parse(idClaim);
        var user = await _uow.Users.GetByIdAsync(id);
        if (user == null) throw new Exception("User not found");

        return new MeResponse
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
        };
    }

    public async Task<TokenResponse> GenerateTokensAsync(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_auth.JwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
                issuer: _auth.Issuer,
                audience: _auth.Audience,
                claims: new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),

                },
                notBefore: now,
                expires: now.AddMinutes(_auth.AccessTokenMinutes),
                signingCredentials: creds
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refresh = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = now.AddDays(_auth.RefreshTokenDays)
        };

        await _uow.RefreshTokens.AddAsync(refresh);
        await _uow.CompleteAsync();

        _httpContext.HttpContext?.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = refresh.ExpiresAt
        });

        return new TokenResponse { AccessToken = accessToken };
    }
}
