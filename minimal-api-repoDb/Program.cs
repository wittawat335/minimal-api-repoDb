using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using minimal_api_repoDb.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<EmployeeRepository>();
//builder.Services.AddScoped<EmployeeQuery>();
//builder.Services.AddScoped<EmployeeMutation>();
//builder.Services.AddScoped<AppSchema>();

builder.Services.AddFastEndpoints().SwaggerDocument();

//GraphQL
// register graphQL
builder.Services.AddGraphQL().AddSystemTextJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EntityDatabaseContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeDB"), 
    sqlServerOptions => sqlServerOptions.CommandTimeout(60)), ServiceLifetime.Transient);

var app = builder.Build();

app.MapGet("/employees", async (EntityDatabaseContext db) =>
    await db.Employees.ToListAsync()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//GraphQL
//app.UseGraphQL<AppSchema>();
app.UseGraphQLGraphiQL("/ui/graphql");
app.UseAuthorization();

app.MapControllers();

app.UseFastEndpoints().UseSwaggerGen();

app.Run();
