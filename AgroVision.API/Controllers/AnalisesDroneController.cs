using AgroVision.Application.DTOs.AnaliseDrone;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalisesDroneController : ControllerBase
{
    private readonly IAnaliseDroneService _analiseDroneService;

    public AnalisesDroneController(IAnaliseDroneService analiseDroneService)
    {
        _analiseDroneService = analiseDroneService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnaliseDroneResponseDto>>> GetAll()
    {
        var analises = await _analiseDroneService.GetAllAsync();

        return Ok(analises);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AnaliseDroneResponseDto>> GetById(int id)
    {
        var analise = await _analiseDroneService.GetByIdAsync(id);

        if (analise is null)
            return NotFound(new { mensagem = "Análise de drone não encontrada." });

        return Ok(analise);
    }

    [HttpGet("plantacao/{plantacaoId:int}")]
    public async Task<ActionResult<IEnumerable<AnaliseDroneResponseDto>>> GetByPlantacaoId(int plantacaoId)
    {
        var analises = await _analiseDroneService.GetByPlantacaoIdAsync(plantacaoId);

        return Ok(analises);
    }

    [HttpGet("criticas")]
    public async Task<ActionResult<IEnumerable<AnaliseDroneResponseDto>>> GetAnalisesCriticas()
    {
        var analises = await _analiseDroneService.GetAnalisesCriticasAsync();

        return Ok(analises);
    }

    [HttpGet("plantacao/{plantacaoId:int}/ultima")]
    public async Task<ActionResult<AnaliseDroneResponseDto>> GetUltimaAnaliseByPlantacaoId(int plantacaoId)
    {
        var analise = await _analiseDroneService.GetUltimaAnaliseByPlantacaoIdAsync(plantacaoId);

        if (analise is null)
            return NotFound(new { mensagem = "Nenhuma análise encontrada para esta plantação." });

        return Ok(analise);
    }

    [HttpPost]
    public async Task<ActionResult<AnaliseDroneResponseDto>> Create([FromBody] AnaliseDroneCreateDto dto)
    {
        var analise = await _analiseDroneService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = analise.Id }, analise);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AnaliseDroneResponseDto>> Update(
        int id,
        [FromBody] AnaliseDroneUpdateDto dto)
    {
        var analise = await _analiseDroneService.UpdateAsync(id, dto);

        if (analise is null)
            return NotFound(new { mensagem = "Análise de drone não encontrada." });

        return Ok(analise);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _analiseDroneService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Análise de drone não encontrada." });

        return NoContent();
    }
}