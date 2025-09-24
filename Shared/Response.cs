namespace Shared;

public class Response<T>
{
    public T? data { get; set; }
    public bool isSuccess { get; set; } = false;
    public string? message { get; set; }
    public string? errorMessage { get; set; }
}
