using Domain.Entities;
using Domain.Interfaces;
using FastEndpoints;
using Shared;

namespace minimal_api_repoDb.Features.Employees.GetEmployees;

public class GetEmployeesEndpoint(IDapperRepository<Employee> _repository) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/api/employees");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var list = await _repository.GetAllAsync();

        var response = new Response<List<Employee>>
        {
            isSuccess = true,
            data = list.ToList()
        };

        await Send.OkAsync(response, ct);
    }
}
