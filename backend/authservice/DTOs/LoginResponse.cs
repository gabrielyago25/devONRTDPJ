namespace authservice.DTOs;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}