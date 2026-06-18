using regservice.Enums;

namespace regservice.DTOs;

public class FiltroRegistroRequest
{
    public TipoRegistro? Tipo { get; set; }
    public StatusRegistro? Status { get; set; }
    public int Pagina { get; set; } = 1;
    public int Limite { get; set; } = 10;
}