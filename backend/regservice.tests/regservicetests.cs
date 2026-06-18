using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using regservice.Data;
using regservice.DTOs;
using regservice.Enums;
using regservice.Exceptions;
using regservice.Services;

namespace regservice.tests;

public class RegistroServiceTests
{
    private static RegDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<RegDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new RegDbContext(options);
    }
    private static RegistroService CriarRegistro(RegDbContext context)
    {
        return new RegistroService(context);
    }

    [Fact]
    public void RegistroCPFValido()
    {
        var contexto = CriarContexto();
        var service = CriarRegistro(contexto);

        var request = new RegistroRequest
        {
            Tipo = TipoRegistro.CONTRATO,
            NomeApresentante = "Teste Registro",
            CpfCnpj = "05251244118",
            DataEntrada = DateOnly.FromDateTime(DateTime.UtcNow),
            Observacoes = "Teste"
        };

        var resultado = service.CriarRegistro(request, Guid.Parse("11111111-1111-1111-1111-111111111111"));

        resultado.Should().NotBeNull();
        resultado.NomeApresentante.Should().Be("Teste Registro");
        contexto.Registros.Count().Should().Be(1);
    }

    [Fact]
    public void RegistroCPFInvalido()
    {
        var contexto = CriarContexto();
        var service = CriarRegistro(contexto);

        var request = new RegistroRequest
        {
            Tipo = TipoRegistro.CONTRATO,
            NomeApresentante = "Teste Registro",
            CpfCnpj = "11111111111",
            DataEntrada = DateOnly.FromDateTime(DateTime.UtcNow),
            Observacoes = "Teste"
        };

        Action act = () => service.CriarRegistro(request, Guid.Parse("11111111-1111-1111-1111-111111111111"));

        act.Should().Throw<Exception>().WithMessage("CPF/CNPJ inválido.");
    }

    [Fact]
    public void PendenteParaRegistradoAtualizaStatus()
    {
        var contexto = CriarContexto();
        var service = CriarRegistro(contexto);

        var registro = service.CriarRegistro(new RegistroRequest
        {
            Tipo = TipoRegistro.CONTRATO,
            NomeApresentante = "Teste Registro",
            CpfCnpj = "52998224725",
            DataEntrada = DateOnly.FromDateTime(DateTime.UtcNow),
            Observacoes = "Teste"
        }, Guid.NewGuid());

        var resultado = service.AtualizarStatus(registro.Id, new AtualizarStatusRequest
        {
            Status = StatusRegistro.REGISTRADO
        });

        resultado.Status.Should().Be(StatusRegistro.REGISTRADO);
    }

    [Fact]
    public void PendenteParaDevolvidoAtualizaStatus()
    {
        var contexto = CriarContexto();
        var service = CriarRegistro(contexto);

        var registro = service.CriarRegistro(new RegistroRequest
        {
            Tipo = TipoRegistro.CONTRATO,
            NomeApresentante = "Teste Registro",
            CpfCnpj = "52998224725",
            DataEntrada = DateOnly.FromDateTime(DateTime.UtcNow),
            Observacoes = "Teste"
        }, Guid.NewGuid());

        var resultado = service.AtualizarStatus(registro.Id, new AtualizarStatusRequest
        {
            Status = StatusRegistro.DEVOLVIDO
        });

        resultado.Status.Should().Be(StatusRegistro.DEVOLVIDO);
    }

    [Fact]
    public void DevolvidoParaPendenteAtualizaStatus()
    {
        var contexto = CriarContexto();
        var service = CriarRegistro(contexto);

        var registro = service.CriarRegistro(new RegistroRequest
        {
            Tipo = TipoRegistro.CONTRATO,
            NomeApresentante = "Teste Registro",
            CpfCnpj = "52998224725",
            DataEntrada = DateOnly.FromDateTime(DateTime.UtcNow),
            Observacoes = "Teste"
        }, Guid.NewGuid());

        service.AtualizarStatus(registro.Id, new AtualizarStatusRequest
        {
            Status = StatusRegistro.DEVOLVIDO
        });

        var resultado = service.AtualizarStatus(registro.Id, new AtualizarStatusRequest
        {
            Status = StatusRegistro.PENDENTE
        });

        resultado.Status.Should().Be(StatusRegistro.PENDENTE);
    }

    [Fact]
    public void RegistradoParaPendenteAtualizaStatus()
    {
        var contexto = CriarContexto();
        var service = CriarRegistro(contexto);

        var registro = service.CriarRegistro(new RegistroRequest
        {
            Tipo = TipoRegistro.CONTRATO,
            NomeApresentante = "Teste Registro",
            CpfCnpj = "52998224725",
            DataEntrada = DateOnly.FromDateTime(DateTime.UtcNow),
            Observacoes = "Teste"
        }, Guid.NewGuid());

        service.AtualizarStatus(registro.Id, new AtualizarStatusRequest
        {
            Status = StatusRegistro.REGISTRADO
        });
        
        Action act = () => service.AtualizarStatus(registro.Id, new AtualizarStatusRequest
        {
            Status = StatusRegistro.PENDENTE
        });

        act.Should().Throw<MudancaStatusIncorreta>();
    }

    [Fact]
    public void BuscaRegistroInexistente()
    {
        var context = CriarContexto();
        var service = CriarRegistro(context);

        var idNaoExiste = Guid.Parse("99999999-9999-9999-9999-999999999999");

        context.Registros.Any(r => r.Id == idNaoExiste).Should().BeFalse();

        Action act = () => service.BuscarPorId(idNaoExiste);

        act.Should().Throw<RegistroNaoEncontrado>();
    }

    [Fact]
    public void ExcluirRegistroRemoveRegistro()
    {
        var context = CriarContexto();
        var service = CriarRegistro(context);

        var registro = service.CriarRegistro(new RegistroRequest
        {
            Tipo = TipoRegistro.CONTRATO,
            NomeApresentante = "Teste Registro",
            CpfCnpj = "52998224725",
            DataEntrada = DateOnly.FromDateTime(DateTime.UtcNow),
            Observacoes = "Teste"
        }, Guid.NewGuid());

        service.ExcluirRegistro(registro.Id);

        context.Registros.Count().Should().Be(0);
    }

}