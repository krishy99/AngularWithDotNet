using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API;

public class TokenService : ITokenService
{
    private readonly IConfiguration config;

    public TokenService(IConfiguration configuration){
        config = configuration;
    }

    public string CreateToken(AppUser user)
    {
        var TokenKey = config["TokenKey"] ?? throw new Exception("Cannot Access Token Key");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));
        var claims = new List<Claim>{
            new(ClaimTypes.NameIdentifier,user.FirstName)
        };
        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds

        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }
}
