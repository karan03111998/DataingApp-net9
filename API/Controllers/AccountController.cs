using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Dto;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDTO loginDTO)
    {
      var user = await context.Users.
                  FirstOrDefaultAsync( x => x.UserName == loginDTO.Username.ToLower());
          if (user == null) return BadRequest("Invalid User");

          var hmac = new HMACSHA512(user.PasswordSalt);
          var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

          for( int i = 0; i < ComputeHash.Length ; i++ )
          {
              if(ComputeHash[i] != user.PasswordHash[i])
              {
                 return BadRequest("invalid username and password");
              }
          }
          return new UserDto
          {
              Username = user.UserName,
              Token = tokenService.CreateToken(user)
          };

    }
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> CreateUser(Register register)
    {
        if (await DuplicateUser(register.Username)) return BadRequest("duplicate User");
        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = register.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
            PasswordSalt = hmac.Key,
        };

        context.Add(user);
        await context.SaveChangesAsync();

        return user;
    }
    public async Task<bool> DuplicateUser( string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
