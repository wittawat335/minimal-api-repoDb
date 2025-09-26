using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Infrastructure;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectInfra();

builder.Services.SwaggerDocument();

builder.Services
    .AddFastEndpoints()
    .AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["Settings:JwtSecret"])
    .AddAuthorization();

builder.Services.AddMapster();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthentication() 
   .UseAuthorization() 
   .UseFastEndpoints()
   .UseSwaggerGen();

app.Run();
