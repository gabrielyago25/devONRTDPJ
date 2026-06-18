using regservice.Enums;

namespace regservice.DTOs;

public class RegistroResponse
{
    public Guid Id { get; set; }
    public TipoRegistro Tipo { get; set; }
    public string NomeApresentante { get; set; } = string.Empty;
    public string CpfCnpj { get; set; } = string.Empty;
    public DateOnly DataEntrada { get; set; }
    public StatusRegistro Status { get; set; }
    public string? Observacoes { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime DataCriado { get; set; }
    public DateTime DataAtualizado { get; set; }
}