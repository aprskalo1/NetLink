using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NetLink.API.Models;

namespace NetLink.API.Services.Auth;

public interface IJwtTokenService
{
    string GenerateToken(Developer? developer = null);
}

public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    public string GenerateToken(Developer? developer = null)
    {
        var jwtSettings = configuration.GetSection("JWT");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();

        if (developer != null)
        {
            claims.Add(new Claim("developerId", developer.Id.ToString()));
            claims.Add(new Claim("username", developer.Username!));
            claims.Add(new Claim("devToken", developer.DevToken!));
            claims.Add(new Claim("active", developer.Active.ToString()));
            claims.Add(new Claim("createdAt", developer.CreatedAt.ToString(CultureInfo.InvariantCulture)));
        }

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpirationInMinutes"]!)),
            signingCredentials: credentials,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}