using regservice.Enums;
using regservice.Models;

namespace regservice.Data;

public static class RegDbSeed
{
    public static void Seed(RegDbContext context)
    {
        if (context.Registros.Any())
            return;

        var registros = new List<Registro>
        {
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Tipo = TipoRegistro.CONTRATO,
                NomeApresentante = "Andrea Vieira",
                CpfCnpj = "52998224725",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today),
                Status = StatusRegistro.PENDENTE,
                Observacoes = "Pendente teste",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Tipo = TipoRegistro.PROCURACAO,
                NomeApresentante = "Dyego Luz LTDA",
                CpfCnpj = "11222333000181",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
                Status = StatusRegistro.REGISTRADO,
                Observacoes = "Registrado teste",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Tipo = TipoRegistro.NOTIFICACAO,
                NomeApresentante = "Rainey Marinho",
                CpfCnpj = "11144477735",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)),
                Status = StatusRegistro.DEVOLVIDO,
                Observacoes = "Devolvido teste",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            }
        };

        context.Registros.AddRange(registros);
        context.SaveChanges();
    }
}