using authservice.Enums;
using authservice.Models;

namespace authservice.Data;

public static class DbSeed
{
    public static void Seed(AuthDbContext context)
    {
    CriarUsuarioPadrao(
        context,
        Guid.Parse("11111111-1111-1111-1111-111111111111"),
        "Administrador",
        "admin@teste.com",
        RoleUsuario.Administrador);

    CriarUsuarioPadrao(
        context,
        Guid.Parse("22222222-2222-2222-2222-222222222222"),
        "Registrador",
        "registrador@teste.com",
        RoleUsuario.Registrador);

    CriarUsuarioPadrao(
        context,
        Guid.Parse("33333333-3333-3333-3333-333333333333"),
        "Consulta",
        "consulta@teste.com",
        RoleUsuario.Consulta);

        context.SaveChanges();
    }

    private static void CriarUsuarioPadrao(
        AuthDbContext context,
        Guid id,
        string nome,
        string email,
        RoleUsuario role)
    {
        if (context.Usuarios.Any(u => u.Email == email))
            return;

        context.Usuarios.Add(new Usuario
        {
            Id = id,
            Nome = nome,
            Email = email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
            Role = role,
            Ativo = true
        });
    }
}