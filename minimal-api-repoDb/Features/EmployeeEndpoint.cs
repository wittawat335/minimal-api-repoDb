using FastEndpoints;
using minimal_api_repoDb.Data.Respositories;

namespace minimal_api_repoDb.Features;

public class EmployeeEndpoint(EmployeeRepository _repository) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/api/employees");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var list = _repository.GetAllEmployees();
        await Send.OkAsync(list);
    }
}
