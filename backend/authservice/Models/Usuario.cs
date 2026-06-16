using authservice.Enums;
namespace authservice.Models;
public class Usuario
{
    public Guid Id { get; set; }
    public string Nome {get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public RoleUsuario Role {get; set; }
    public bool Ativo { get; set; } = true;
}