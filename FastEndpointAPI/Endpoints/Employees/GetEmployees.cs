using Application.Dtos.Employees;
using Domain.Entities;
using Domain.Interfaces;
using FastEndpoints;
using Shared;

namespace FastEndpointAPI.Endpoints.Employees;

public class GetEmployees(IDapperRepository<Employee> _repository, IMapper _mapper) : EndpointWithoutRequest
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
