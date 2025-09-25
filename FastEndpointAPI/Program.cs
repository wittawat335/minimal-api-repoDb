using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectInfra();

builder.Services.AddFastEndpoints().SwaggerDocument();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseFastEndpoints().UseSwaggerGen();

app.Run();
