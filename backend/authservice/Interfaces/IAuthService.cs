using authservice.DTOs;
using authservice.Models;
namespace authservice.Interfaces;

public interface IAuthService
{
    LoginResponse Login(LoginRequest request);
    void Register(RegisterRequest request);
    void AlterarRole(Guid usuarioId, AlterarRoleRequest request);
     List<Usuario> ListarUsuarios();
}