using AgroVision.Application.DTOs.Plantacao;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantacoesController : ControllerBase
{
    private readonly IPlantacaoService _plantacaoService;

    public PlantacoesController(IPlantacaoService plantacaoService)
    {
        _plantacaoService = plantacaoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlantacaoResponseDto>>> GetAll()
    {
        var plantacoes = await _plantacaoService.GetAllAsync();

        return Ok(plantacoes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlantacaoResponseDto>> GetById(int id)
    {
        var plantacao = await _plantacaoService.GetByIdAsync(id);

        if (plantacao is null)
            return NotFound(new { mensagem = "Plantação não encontrada." });

        return Ok(plantacao);
    }

    [HttpGet("usuario/{usuarioId:int}")]
    public async Task<ActionResult<IEnumerable<PlantacaoResponseDto>>> GetByUsuarioId(int usuarioId)
    {
        var plantacoes = await _plantacaoService.GetByUsuarioIdAsync(usuarioId);

        return Ok(plantacoes);
    }

    [HttpGet("ativas")]
    public async Task<ActionResult<IEnumerable<PlantacaoResponseDto>>> GetAtivas()
    {
        var plantacoes = await _plantacaoService.GetAtivasAsync();

        return Ok(plantacoes);
    }

    [HttpPost]
    public async Task<ActionResult<PlantacaoResponseDto>> Create([FromBody] PlantacaoCreateDto dto)
    {
        var plantacao = await _plantacaoService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = plantacao.Id }, plantacao);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PlantacaoResponseDto>> Update(
        int id,
        [FromBody] PlantacaoUpdateDto dto)
    {
        var plantacao = await _plantacaoService.UpdateAsync(id, dto);

        if (plantacao is null)
            return NotFound(new { mensagem = "Plantação não encontrada." });

        return Ok(plantacao);
    }

    [HttpPatch("{id:int}/encerrar")]
    public async Task<ActionResult> Encerrar(int id)
    {
        var atualizado = await _plantacaoService.EncerrarAsync(id);

        if (!atualizado)
            return NotFound(new { mensagem = "Plantação não encontrada." });

        return NoContent();
    }

    [HttpPatch("{id:int}/reativar")]
    public async Task<ActionResult> Reativar(int id)
    {
        var atualizado = await _plantacaoService.ReativarAsync(id);

        if (!atualizado)
            return NotFound(new { mensagem = "Plantação não encontrada." });

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _plantacaoService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Plantação não encontrada." });

        return NoContent();
    }
}