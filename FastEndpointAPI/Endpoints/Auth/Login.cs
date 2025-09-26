using Application.Dtos.Register;
using FastEndpoints;
using Shared;

namespace FastEndpointAPI.Endpoints.Auth;

public class Login : Endpoint<RegisterRequest, Response<RegisterResponse>>
{
    public override void Configure()
    {
        Post("/api/auth/login");
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
        };

        await Send.OkAsync(response, cancellation: ct);
    }
}
