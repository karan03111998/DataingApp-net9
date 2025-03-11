using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interface;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = configuration["Tokenkey"] ?? throw new Exception("cannot acccess taken key");
        if(tokenKey.Length < 64) throw new Exception("cannot acccess taken key");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier,user.UserName)
        };

        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };
         
         var tokenHandler = new JwtSecurityTokenHandler();

         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
    }
}
