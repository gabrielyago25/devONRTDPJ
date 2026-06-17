using authservice.DTOs;
using authservice.Interfaces;
using authservice.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using authservice.Enums;
using authservice.Data;

namespace authservice.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly AuthDbContext _context;

    public AuthService(IConfiguration config, AuthDbContext context)
    {
        _config = config;
        _context = context;
    }
 /*   private static void CriarUsuariosPadrao()
    {
        if (Usuarios.Any())
            return;

        Usuarios.Add(new Usuario
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Nome = "Administrador",
            Email = "admin@teste.com",
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
            Role = RoleUsuario.Administrador,
            Ativo = true
        });
    }
*/
    public void Register(RegisterRequest request)
    {
        var emailExistente = _context.Usuarios.Any(u => u.Email == request.Email);

        if (emailExistente)
            throw new Exception("E-mail já cadastrado.");

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            Role = RoleUsuario.Consulta,
            Ativo = true,
        };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public LoginResponse Login(LoginRequest request)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == request.Email);
        if (usuario == null)
            throw new Exception("Dados de acesso incorretos.");
        var senhaUsuario = BCrypt.Net.BCrypt.Verify(
            request.Senha,
            usuario.SenhaHash
        );
        if (!senhaUsuario)
            throw new Exception("Dados de acesso incorretos.");

        var expirarToken = DateTime.UtcNow.AddHours(1);
        return new LoginResponse
        {
            AccessToken = GerarToken(usuario, expirarToken),
            Expiration = expirarToken
        };
    }
    private string GerarToken(Usuario usuario, DateTime expirarToken)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, usuario.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expirarToken,
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }   
    public void AlterarRole(Guid usuarioId, AlterarRoleRequest request)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

        if (usuario == null)
            throw new Exception("Dados não encontrados.");

        usuario.Role = request.Role;
        _context.SaveChanges();
    }
    public List<Usuario> ListarUsuarios()
    {
        return _context.Usuarios.ToList();
    }
}