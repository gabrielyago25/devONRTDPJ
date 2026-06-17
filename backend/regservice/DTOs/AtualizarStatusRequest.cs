using regservice.Enums;
using System.ComponentModel.DataAnnotations;

namespace regservice.DTOs;

public class AtualizarStatusRequest
{
    [Required]
    public StatusRegistro Status { get; set; }
}