using API.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ApiServiceExtension(builder.Configuration);
builder.Services.AddIdentityServiceExtension(builder.Configuration);

var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200","https://localhost:4200"));
// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
