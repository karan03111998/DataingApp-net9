using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions option) : DbContext(option)
{
    public DbSet<AppUser> Users {get;set;}
}
