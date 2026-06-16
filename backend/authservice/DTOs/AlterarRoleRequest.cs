using authservice.Enums;
using System.ComponentModel.DataAnnotations;

namespace authservice.DTOs;

public class AlterarRoleRequest
{
    [Required]
    public RoleUsuario Role { get; set; }
}