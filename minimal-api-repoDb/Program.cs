using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using minimal_api_repoDb.Data;
using minimal_api_repoDb.Data.Respositories;
using minimal_api_repoDb.GraphQL;
using minimal_api_repoDb.GraphQL.Mutations;
using minimal_api_repoDb.GraphQL.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Repositories & GraphQL
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeQuery>();
builder.Services.AddScoped<EmployeeMutation>();
builder.Services.AddScoped<AppSchema>();

// FastEndpoints + Swagger //
builder.Services.AddFastEndpoints()
                .SwaggerDocument();

// GraphQL
builder.Services.AddGraphQL()
                .AddSystemTextJson();

// Swagger (API Explorer)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DbContext
builder.Services.AddDbContext<EntityDatabaseContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("EmployeDB"),
        sqlServerOptions => sqlServerOptions.CommandTimeout(60)
    ),
    ServiceLifetime.Transient
);

var app = builder.Build();

// Minimal API sample
app.MapGet("/employees", async (EntityDatabaseContext db) =>
    await db.Employees.ToListAsync()
);

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GraphQL
app.UseGraphQL<AppSchema>();
app.UseGraphQLGraphiQL("/ui/graphql");

app.UseAuthorization();

app.UseFastEndpoints()
   .UseSwaggerGen();

app.Run();
