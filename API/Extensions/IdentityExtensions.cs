using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;


public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityExtensions(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenkey = Configuration["TokenKey"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
        ValidateIssuer = false,
        ValidateAudience = false
        };
    });
    return Services;
    }


}

