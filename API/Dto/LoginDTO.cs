using System;

namespace API.Dto;

public class LoginDTO
{
  public required string Username { get; set; }
  public required string Password { get; set; }
}
