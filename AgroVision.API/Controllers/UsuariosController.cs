using AgroVision.Application.DTOs.Usuario;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetAll()
    {
        var usuarios = await _usuarioService.GetAllAsync();

        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioResponseDto>> GetById(int id)
    {
        var usuario = await _usuarioService.GetByIdAsync(id);

        if (usuario is null)
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDto>> Create([FromBody] UsuarioCreateDto dto)
    {
        var usuario = await _usuarioService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UsuarioResponseDto>> Update(
        int id,
        [FromBody] UsuarioUpdateDto dto)
    {
        var usuario = await _usuarioService.UpdateAsync(id, dto);

        if (usuario is null)
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(usuario);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _usuarioService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return NoContent();
    }
}