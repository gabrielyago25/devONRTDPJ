using authservice.Data;
using authservice.DTOs;
using authservice.Enums;
using authservice.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace authservice.tests;

public class AuthServiceTests
{
    private static AuthDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AuthDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AuthDbContext(options);
    }

    private static IConfiguration CriarConfiguracao()
    {
        var dados = new Dictionary<string, string?>
        {
            { "Jwt:Key", "string-teste-12345678901234567-2" },
            { "Jwt:Issuer", "AuthService" },
            { "Jwt:Audience", "AuthServiceUsers" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(dados)
            .Build();
    }

    [Fact]
    public void Register_CadastroUsuarioComRolePadrao()
    {
        var context = CriarContexto();
        var config = CriarConfiguracao();
        var service = new AuthService(config, context);

        var request = new RegisterRequest
        {
            Nome = "Usuário Teste",
            Email = "teste@email.com",
            Senha = "12345678"
        };

        service.Register(request);

        var usuario = context.Usuarios.First();

        usuario.Email.Should().Be("teste@email.com");
        usuario.Role.Should().Be(RoleUsuario.Consulta);
        usuario.SenhaHash.Should().NotBe("12345678");
        BCrypt.Net.BCrypt.Verify("12345678", usuario.SenhaHash).Should().BeTrue();
    }

    [Fact]
    public void LoginValido_RetornaToken()
    {
        var context = CriarContexto();
        var config = CriarConfiguracao();
        var service = new AuthService(config, context);

        service.Register(new RegisterRequest
        {
            Nome = "Usuário Teste",
            Email = "teste@email.com",
            Senha = "12345678"
        });

        var response = service.Login(new LoginRequest
        {
            Email = "teste@email.com",
            Senha = "12345678"
        });

        response.AccessToken.Should().NotBeNullOrWhiteSpace();
        response.Expiration.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact]
    public void LoginSenhaInvalida_RetornaExcecao()
    {
        var context = CriarContexto();
        var config = CriarConfiguracao();
        var service = new AuthService(config, context);

        service.Register(new RegisterRequest
        {
            Nome = "Usuário Teste",
            Email = "teste@email.com",
            Senha = "12345678"
        });

        var acao = () => service.Login(new LoginRequest
        {
            Email = "teste@email.com",
            Senha = "senhaerrada"
        });

        acao.Should().Throw<Exception>()
            .WithMessage("Dados de acesso incorretos.");
    }

    [Fact]
    public void Register_EmailRepetido_RetornaExcecao()
    {
        var context = CriarContexto();
        var config = CriarConfiguracao();
        var service = new AuthService(config, context);

        var request = new RegisterRequest
        {
            Nome = "Usuário Teste",
            Email = "teste@email.com",
            Senha = "12345678"
        };

        service.Register(request);

        var acao = () => service.Register(request);

        acao.Should().Throw<Exception>()
            .WithMessage("E-mail já cadastrado.");
    }

    [Fact]
    public void AlterarRole_AlteraRole()
    {
        var context = CriarContexto();
        var config = CriarConfiguracao();
        var service = new AuthService(config, context);

        service.Register(new RegisterRequest
        {
            Nome = "Usuário Teste",
            Email = "teste@email.com",
            Senha = "12345678"
        });

        var usuario = context.Usuarios.First();

        service.AlterarRole(usuario.Id, new AlterarRoleRequest
        {
            Role = RoleUsuario.Administrador
        });

        usuario.Role.Should().Be(RoleUsuario.Administrador);
    }
}