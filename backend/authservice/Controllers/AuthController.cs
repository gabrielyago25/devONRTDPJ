using authservice.DTOs;
using authservice.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace authservice.Controllers;

[ApiController]
[Route("api/auth")]

// entrada da API de login 
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController (IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        try {
        var retorno = _authService.Login(request);
        return Ok(retorno);
        }
        catch
        {
            return Unauthorized(new {mensagem = "Dados de acesso incorretos."});
        }
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        _authService.Register(request);
        return Ok("Usuário cadastrado com sucesso.");
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            Mensagem = "Você acessou uma rota protegida.",
            Usuario = User.Identity?.Name
        });
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("admin")]
    public IActionResult Admin()
    {
        return Ok(new
        {
            Mensagem = "Você acessou uma rota exclusiva para Administrador.",
            Usuario = User.Identity?.Name
        });
    }

}