using authservice.Enums;

namespace authservice.DTOs;

public class UsuarioResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public RoleUsuario Role { get; set; }
    public bool Ativo { get; set; }
}