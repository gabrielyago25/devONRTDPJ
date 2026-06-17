using regservice.Enums;

namespace regservice.Models;

public class Registro
{
    public Guid Id { get; set; }

    public TipoRegistro Tipo { get; set; }

    public string NomeApresentante { get; set; } = string.Empty;

    public string CpfCnpj { get; set; } = string.Empty;

    public DateOnly DataEntrada { get; set; }

    public StatusRegistro Status { get; set; } = StatusRegistro.PENDENTE;

    public string? Observacoes { get; set; }

    public Guid CriadoPor { get; set; }

    public DateTime DataCriado { get; set; } = DateTime.UtcNow;

    public DateTime DataAtualizado { get; set; } = DateTime.UtcNow;
}