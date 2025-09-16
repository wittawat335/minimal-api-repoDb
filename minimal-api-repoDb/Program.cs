using Domain.DBContext;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

var bld = WebApplication.CreateBuilder();
bld.Services.AddFastEndpoints().SwaggerDocument();

bld.Services.AddAuthenticationJwtBearer(_ => _.SigningKey = bld.Configuration["JwtSecret"]);
bld.Services.AddAuthentication();

bld.Services.AddSingleton<DapperContext>();

var app = bld.Build();
app.UseFastEndpoints().UseSwaggerGen(); 
app.UseAuthentication().UseAuthorization();
app.Run();
