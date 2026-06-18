using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using regservice.DTOs;
using regservice.Interfaces;

namespace regservice.Controllers;

[ApiController]
[Route("api/registros")]
[Authorize]
public class RegistroController : ControllerBase
{
    private readonly IRegistroService _registroService;

    public RegistroController(IRegistroService registroService)
    {
        _registroService = registroService;
    }
    // criar registro - autorização administrador e registrador
    [Authorize(Roles = "Administrador, Registrador")]
    [HttpPost]
    public IActionResult CriarRegistro(RegistroRequest request)
    {
        var usuarioId = ObterUsuarioIdToken();
        var response = _registroService.CriarRegistro(request, usuarioId);
        return CreatedAtAction(nameof(BuscarPorId), new {id = response.Id}, response);
    }
    // listar registros - autorização administrador, registrador e consulta
    [Authorize(Roles = "Administrador,Registrador,Consulta")]
    [HttpGet]
    public IActionResult ListarRegistros()
    {
        var registros = _registroService.ListarRegistros();
        return Ok(registros);
    }
    // buscar registro por id - autorização administrador, registrador e consulta
    [Authorize(Roles = "Administrador,Registrador,Consulta")]
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        var registro = _registroService.BuscarPorId(id);
        return Ok(registro);
    }
    // atualização de registro - autorização administrador e registrador
    [Authorize(Roles = "Administrador,Registrador")]
    [HttpPut("{id}")]
    public IActionResult AtualizarRegistro(Guid id, AtualizarRegistroRequest request)
    {
        var registro = _registroService.AtualizarRegistro(id, request);
        return Ok(registro);
    }
    // atualização de status - autorização administrador e registrador
    [Authorize(Roles = "Administrador,Registrador")]
    [HttpPatch("{id}/status")]
    public IActionResult AtualizarStatus(Guid id, AtualizarStatusRequest request)
    {
        var registro = _registroService.AtualizarStatus(id, request);
        return Ok(registro);
    }
    // deletar registro - autorização administrador
    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public IActionResult ExcluirRegistro(Guid id)
    {
        _registroService.ExcluirRegistro(id);
        return NoContent();
}
    private Guid ObterUsuarioIdToken()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)?? User.FindFirstValue("sub");
        
        if (string.IsNullOrWhiteSpace(sub))
            throw new Exception ("Usuário não identificado no token.");

        return Guid.Parse(sub);
    }

}