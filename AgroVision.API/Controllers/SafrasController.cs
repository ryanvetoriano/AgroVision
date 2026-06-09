using AgroVision.Application.DTOs.Safra;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SafrasController : ControllerBase
{
    private readonly ISafraService _safraService;

    public SafrasController(ISafraService safraService)
    {
        _safraService = safraService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SafraResponseDto>>> GetAll()
    {
        var safras = await _safraService.GetAllAsync();

        return Ok(safras);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SafraResponseDto>> GetById(int id)
    {
        var safra = await _safraService.GetByIdAsync(id);

        if (safra is null)
            return NotFound(new { mensagem = "Safra não encontrada." });

        return Ok(safra);
    }

    [HttpGet("plantacao/{plantacaoId:int}")]
    public async Task<ActionResult<IEnumerable<SafraResponseDto>>> GetByPlantacaoId(int plantacaoId)
    {
        var safras = await _safraService.GetByPlantacaoIdAsync(plantacaoId);

        return Ok(safras);
    }

    [HttpPost]
    public async Task<ActionResult<SafraResponseDto>> Create([FromBody] SafraCreateDto dto)
    {
        var safra = await _safraService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = safra.Id }, safra);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<SafraResponseDto>> Update(
        int id,
        [FromBody] SafraUpdateDto dto)
    {
        var safra = await _safraService.UpdateAsync(id, dto);

        if (safra is null)
            return NotFound(new { mensagem = "Safra não encontrada." });

        return Ok(safra);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _safraService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Safra não encontrada." });

        return NoContent();
    }
}