using Core.Configuration;
using Microsoft.Extensions.Options;

namespace Services;

public class TokenService
{
    private readonly AuthOptions _auth;

    public TokenService(IOptions<AuthOptions> options)
    {
        _auth = options.Value;
    }
}
