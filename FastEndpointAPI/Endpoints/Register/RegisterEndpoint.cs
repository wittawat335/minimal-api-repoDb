using FastEndpoints;
using Shared;

namespace FastEndpointAPI.Endpoints.Register;

public class RegisterEndpoint : Endpoint<RegisterRequest, Response<RegisterResponse>>
{
    public override void Configure()
    {
        Post("/api/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        var user = new RegisterResponse
        {
            UserId = 123,
            Username = req.Username,
            Email = req.Email
        };

        var response = new Response<RegisterResponse>
        {
            data = user,
            isSuccess = true,
            message = "User registered successfully"
        };

        await Send.OkAsync(response, cancellation: ct);
    }
}
