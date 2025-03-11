using System;

namespace API.Dto;

public class Register
{
   public required string Username { get; set; }
   public required string Password { get; set; }
}
