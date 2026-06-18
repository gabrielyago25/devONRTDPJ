using Microsoft.AspNetCore.Mvc.RazorPages;
using regservice.Data;
using regservice.DTOs;
using regservice.Enums;
using regservice.Interfaces;
using regservice.Models;
using regservice.Validators;

namespace regservice.Services;

public class RegistroService : IRegistroService
{
    private readonly RegDbContext _context;
    public RegistroService(RegDbContext context)
    {
        _context = context;
    }
    public RegistroResponse CriarRegistro(RegistroRequest request, Guid usuarioId)
    {
        if (!CpfCnpjValidador.Valido(request.CpfCnpj))
            throw new Exception("CPF/CNPJ inválido.");
        var registro = new Registro
        {
            Id = Guid.NewGuid(),
            Tipo  = request.Tipo,
            NomeApresentante = request.NomeApresentante,
            DataEntrada = request.DataEntrada,
            Status = StatusRegistro.PENDENTE,
            Observacoes = request.Observacoes,
            CriadoPor = usuarioId,
            DataCriado = DateTime.UtcNow,
            DataAtualizado = DateTime.UtcNow,
        };
        _context.Registros.Add(registro);
        _context.SaveChanges();
        return MapearResponse(registro);
    }
    public IEnumerable<RegistroResponse> ListarRegistros(FiltroRegistroRequest filtro)
    {
        var consulta = _context.Registros.AsQueryable();

        if (filtro.Tipo.HasValue)
            consulta = consulta.Where(r => r.Tipo == filtro.Tipo.Value);

        if (filtro.Status.HasValue)
            consulta = consulta.Where(r => r.Status == filtro.Status.Value);

        var pagina = filtro.Pagina< 1 ? 1 : filtro.Pagina;
        var limite = filtro.Limite< 1 ? 10 : filtro.Limite;


        return consulta
        .OrderByDescending(r => r.DataCriado)
        .Skip((pagina -1) * limite)
        .Take(limite)
        .Select(registro => MapearResponse(registro))
        .ToList();
    }
    public RegistroResponse BuscarPorId(Guid id)
    {
        var registro = _context.Registros.FirstOrDefault(r => r.Id == id);
        if (registro == null)
            throw new Exception("Registro não encontrado");

        return MapearResponse(registro);
    }
    public RegistroResponse AtualizarRegistro (Guid id, AtualizarRegistroRequest request)
    {
        var registro = _context.Registros.FirstOrDefault(r => r.Id == id);
        if(registro == null)
            throw new Exception("Registro não encontrado");

        registro.Tipo = request.Tipo;
        registro.NomeApresentante = request.NomeApresentante;
        registro.CpfCnpj = request.CpfCnpj;
        registro.DataEntrada = request.DataEntrada;
        registro.Observacoes = request.Observacoes;
        registro.DataAtualizado = DateTime.UtcNow;

        _context.SaveChanges();
        return MapearResponse(registro);
    }
    public RegistroResponse AtualizarStatus (Guid id, AtualizarStatusRequest request)
    {
        var registro = _context.Registros.FirstOrDefault(r => r.Id == id);
        if (registro == null)
            throw new Exception("Registro não encontrado");
        
        if (!MudancaStatusValida(registro.Status, request.Status))
            throw new Exception("Mudança de status inválida.");

        registro.Status = request.Status;
        registro.DataAtualizado = DateTime.UtcNow;

        _context.SaveChanges();
        return MapearResponse(registro);
    }

    public void ExcluirRegistro(Guid id)
    {
        var registro = _context.Registros.FirstOrDefault(r => r.Id == id);

        if (registro == null)
            throw new Exception("Registro não encontrado");
        
        _context.Registros.Remove(registro);
        _context.SaveChanges();
    }
    private static RegistroResponse MapearResponse (Registro registro)
    {
        return new RegistroResponse
        {
            Id = registro.Id,
            Tipo = registro.Tipo,
            NomeApresentante = registro.NomeApresentante,
            CpfCnpj = registro.CpfCnpj,
            DataEntrada = registro.DataEntrada,
            Status = registro.Status,
            Observacoes = registro.Observacoes,
            CriadoPor = registro.CriadoPor,
            DataCriado = registro.DataCriado,
            DataAtualizado = registro.DataAtualizado
        };
    }
    private static bool MudancaStatusValida(StatusRegistro statusAtual, StatusRegistro novoStatus)
    {
        return (statusAtual, novoStatus) switch
        {
            (StatusRegistro.PENDENTE, StatusRegistro.REGISTRADO) => true,
            (StatusRegistro.PENDENTE, StatusRegistro.DEVOLVIDO) => true,
            (StatusRegistro.DEVOLVIDO, StatusRegistro.PENDENTE) => true,

            _ => false
        };
    }
}