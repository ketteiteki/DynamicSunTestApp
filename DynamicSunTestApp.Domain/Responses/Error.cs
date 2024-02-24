namespace DynamicSunTestApp.Domain.Responses;

public class Error(string message)
{
    public string Message { get; set; } = message;
}