using regservice.Enums;
using System.ComponentModel.DataAnnotations;

namespace regservice.DTOs;

public class RegistroRequest
{
    [Required]
    public TipoRegistro Tipo {get; set;}

    [Required]
    public string NomeApresentante {get; set;} = string.Empty;

    [Required]
    public string CpfCnpj {get; set;} = string.Empty;

    [Required]
    public DateOnly DataEntrada{get; set;}
    public string? Observacoes {get; set;} 

}