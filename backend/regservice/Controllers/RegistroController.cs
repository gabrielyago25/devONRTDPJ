using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using regservice.DTOs;
using regservice.Interfaces;
using regservice.Exceptions;

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
    [Authorize(Roles = "Administrador,Registrador")]
    [HttpPost]
    public IActionResult CriarRegistro(RegistroRequest request)
    {
        try 
        {
            var usuarioId = ObterUsuarioIdToken();
            var response = _registroService.CriarRegistro(request, usuarioId);
            return CreatedAtAction(nameof(BuscarPorId), new {id = response.Id}, response);
        }
        catch (Exception ex)
        {
            return BadRequest(new {mensagem = ex.Message});
        }
    }
    // listar registros - autorização administrador, registrador e consulta
    [Authorize(Roles = "Administrador,Registrador,Consulta")]
    [HttpGet]
    public IActionResult ListarRegistros([FromQuery] FiltroRegistroRequest filtro)
    {
        var registros = _registroService.ListarRegistros(filtro);
        return Ok(registros);
    }
    // buscar registro por id - autorização administrador, registrador e consulta
    [Authorize(Roles = "Administrador,Registrador,Consulta")]
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            var registro = _registroService.BuscarPorId(id);
            return Ok(registro);   
        } catch (RegistroNaoEncontrado ex)
        {
            return NotFound(new {mensagem = ex.Message});
        }
    }
    // atualização de registro - autorização administrador e registrador
    [Authorize(Roles = "Administrador,Registrador")]
    [HttpPut("{id}")]
    public IActionResult AtualizarRegistro(Guid id, AtualizarRegistroRequest request)
    {
        try {
            var registro = _registroService.AtualizarRegistro(id, request);
            return Ok(registro);
        }
        catch (RegistroNaoEncontrado ex)
        {
            return NotFound(new {mensagem = ex.Message});
        } 
        catch (Exception ex)
        {
            return BadRequest(new {mensagem = ex.Message});
        }
    }
    // atualização de status - autorização administrador e registrador
    [Authorize(Roles = "Administrador,Registrador")]
    [HttpPatch("{id}/status")]
    public IActionResult AtualizarStatus(Guid id, AtualizarStatusRequest request)
    {
        try
        {
            var registro = _registroService.AtualizarStatus(id, request);
            return Ok(registro);
        }
        catch (RegistroNaoEncontrado ex)
        {
            return NotFound(new {mensagem = ex.Message });
        }
        catch (MudancaStatusIncorreta ex)
        {
            return UnprocessableEntity(new {mensagem = ex.Message });
        }
    }
    // deletar registro - autorização administrador
    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public IActionResult ExcluirRegistro(Guid id)
    {
        try
        {
            _registroService.ExcluirRegistro(id);
            return NoContent();
        }
        catch (RegistroNaoEncontrado ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
    private Guid ObterUsuarioIdToken()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)?? User.FindFirstValue("sub");
        
        if (string.IsNullOrWhiteSpace(sub))
            throw new Exception ("Usuário não identificado (token).");

        return Guid.Parse(sub);
    }

}