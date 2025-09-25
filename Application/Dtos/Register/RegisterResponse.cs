namespace Application.Dtos.Register;

public class RegisterResponse
{
    public int UserId { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
}
