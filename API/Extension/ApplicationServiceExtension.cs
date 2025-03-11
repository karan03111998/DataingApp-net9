using System;
using API.Data;
using API.Interface;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extension;

public static class ApplicationServiceExtension
{
    public static IServiceCollection ApiServiceExtension(this IServiceCollection service, IConfiguration config)
    {
        service.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        service.AddOpenApi();
        service.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        service.AddCors();
        service.AddScoped<ITokenService, TokenService>();
        return service;
    }
}
