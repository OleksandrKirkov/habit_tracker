namespace Core.Configuration;

public class AuthOptions
{
    public const string Section = "Auth";

    public string JwtSecret { get; set; } = null!;
    public string Issuer { get; set; } = "habit-api";
    public string Audience { get; set; } = "habit-client";
    public int AccessTokenMinutes { get; set; } = 15;
    public int RefreshTokenDays { get; set; } = 7;
}
