using FastEndpoints;

namespace minimal_api_repoDb.Features;

public class HealthEndpoint : Endpoint<HealthRequest, HealthResponse>
{
    public override void Configure()
    {
        Get("/api/Health"); 
        AllowAnonymous();
    }

    public override async Task<HealthResponse> ExecuteAsync(HealthRequest req, CancellationToken ct)
    {
        return new HealthResponse { AllCaps = req.Check };
    }
}

public record HealthRequest
{
    public string Check { get; set; } = "";
}

public record HealthResponse
{
    public string AllCaps { get; set; } = "";
}

