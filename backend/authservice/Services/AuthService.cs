using authservice.DTOs;
using authservice.Interfaces;
using authservice.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace authservice.Services;

public class AuthService : IAuthService
{
    private static readonly List<Usuario> Usuarios = new();
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
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
    public LoginResponse Login(LoginRequest request)
    {
        var usuario = Usuarios.FirstOrDefault(u => u.Email == request.Email);
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
    public void Register(RegisterRequest request)
    {
        var emailExistente = Usuarios.Any(u => u.Email == request.Email);

        if (emailExistente)
            throw new Exception("E-mail já cadastrado.");

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            Role = request.Role,
            Ativo = true,
        };

        Usuarios.Add(usuario);
    }
}