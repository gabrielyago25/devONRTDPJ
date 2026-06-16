using authservice.DTOs;
namespace authservice.Interfaces;

public interface IAuthService
{
    LoginResponse Login(LoginRequest request);
    void Register(RegisterRequest request);
}