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
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Tipo = TipoRegistro.CONTRATO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345678143",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-3)),
                Status = StatusRegistro.PENDENTE,
                Observacoes = "Contrato seed 1",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Tipo = TipoRegistro.CONTRATO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345671000173",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-4)),
                Status = StatusRegistro.REGISTRADO,
                Observacoes = "Contrato seed 2",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                Tipo = TipoRegistro.CONTRATO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345678224",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-5)),
                Status = StatusRegistro.DEVOLVIDO,
                Observacoes = "Contrato seed 3",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                Tipo = TipoRegistro.CONTRATO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345672000118",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-6)),
                Status = StatusRegistro.PENDENTE,
                Observacoes = "Contrato seed 4",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                Tipo = TipoRegistro.CONTRATO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345678305",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-7)),
                Status = StatusRegistro.REGISTRADO,
                Observacoes = "Contrato seed 5",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                Tipo = TipoRegistro.PROCURACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345673000162",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-8)),
                Status = StatusRegistro.PENDENTE,
                Observacoes = "Procuração seed 1",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                Tipo = TipoRegistro.PROCURACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345678496",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-9)),
                Status = StatusRegistro.REGISTRADO,
                Observacoes = "Procuração seed 2",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                Tipo = TipoRegistro.PROCURACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345674000107",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-10)),
                Status = StatusRegistro.DEVOLVIDO,
                Observacoes = "Procuração seed 3",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Tipo = TipoRegistro.PROCURACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345678577",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-11)),
                Status = StatusRegistro.PENDENTE,
                Observacoes = "Procuração seed 4",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                Tipo = TipoRegistro.PROCURACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345675000151",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-12)),
                Status = StatusRegistro.REGISTRADO,
                Observacoes = "Procuração seed 5",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Tipo = TipoRegistro.NOTIFICACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345678658",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-13)),
                Status = StatusRegistro.PENDENTE,
                Observacoes = "Notificação seed 1",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                Tipo = TipoRegistro.NOTIFICACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345676000104",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-14)),
                Status = StatusRegistro.REGISTRADO,
                Observacoes = "Notificação seed 2",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                Tipo = TipoRegistro.NOTIFICACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345678739",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-15)),
                Status = StatusRegistro.DEVOLVIDO,
                Observacoes = "Notificação seed 3",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                Tipo = TipoRegistro.NOTIFICACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345677000140",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-16)),
                Status = StatusRegistro.PENDENTE,
                Observacoes = "Notificação seed 4",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            },

            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                Tipo = TipoRegistro.NOTIFICACAO,
                NomeApresentante = "Teste",
                CpfCnpj = "12345678810",
                DataEntrada = DateOnly.FromDateTime(DateTime.Today.AddDays(-17)),
                Status = StatusRegistro.REGISTRADO,
                Observacoes = "Notificação seed 5",
                CriadoPor = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DataCriado = DateTime.UtcNow,
                DataAtualizado = DateTime.UtcNow
            }
        };

        context.Registros.AddRange(registros);
        context.SaveChanges();
    }
}
