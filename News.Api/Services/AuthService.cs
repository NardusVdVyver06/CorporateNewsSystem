using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using News.Api.Models;
using News.Shared;

namespace News.Api.Services;

public class AuthService(IConfiguration configuration)
{
    public IResult AdminLogin(LoginRequest request)
    {
        try
        {
            // NOTE: This should ideally be in a DB, but just adding it here for the purposes of the Work Item
            if (request.Username != "admin" || request.Password != "Password123!")
            {
                return Results.Unauthorized();
            }
            
            var jwt = configuration.GetSection("Jwt");

            var expiryMinutes = int.Parse(jwt["ExpiryMinutes"]!);

            var expires = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, request.Username),
                new(ClaimTypes.Role, AuthorizationRoles.Admin)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

            var credentials = new SigningCredentials
            (
                key,
                SecurityAlgorithms.HmacSha256
            );

            
            var token = new JwtSecurityToken
            (
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );
            
            return Results.Ok(new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = expires
            });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
