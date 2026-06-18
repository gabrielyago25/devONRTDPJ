using regservice.DTOs;

namespace regservice.Interfaces;

public interface IRegistroService
{
    RegistroResponse CriarRegistro(RegistroRequest request, Guid usuarioId);
    IEnumerable<RegistroResponse> ListarRegistros();
    RegistroResponse BuscarPorId(Guid id);
    RegistroResponse AtualizarRegistro(Guid id, AtualizarRegistroRequest request);
    RegistroResponse AtualizarStatus(Guid id, AtualizarStatusRequest request);
}