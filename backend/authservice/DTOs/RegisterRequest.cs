using System.ComponentModel.DataAnnotations;
using authservice.Enums;

namespace authservice.DTOs;

public class RegisterRequest
{
    [Required]
    [MinLength(3)]
    public string Nome { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(8)]
    public string Senha { get; set; } = string.Empty;
    [Required]
    public RoleUsuario Role { get; set; }
}