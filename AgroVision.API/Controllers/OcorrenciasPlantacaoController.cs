using AgroVision.Application.DTOs.OcorrenciaPlantacao;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OcorrenciasPlantacaoController : ControllerBase
{
    private readonly IOcorrenciaPlantacaoService _ocorrenciaService;

    public OcorrenciasPlantacaoController(IOcorrenciaPlantacaoService ocorrenciaService)
    {
        _ocorrenciaService = ocorrenciaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OcorrenciaPlantacaoResponseDto>>> GetAll()
    {
        var ocorrencias = await _ocorrenciaService.GetAllAsync();

        return Ok(ocorrencias);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OcorrenciaPlantacaoResponseDto>> GetById(int id)
    {
        var ocorrencia = await _ocorrenciaService.GetByIdAsync(id);

        if (ocorrencia is null)
            return NotFound(new { mensagem = "Ocorrência de plantação não encontrada." });

        return Ok(ocorrencia);
    }

    [HttpGet("plantacao/{plantacaoId:int}")]
    public async Task<ActionResult<IEnumerable<OcorrenciaPlantacaoResponseDto>>> GetByPlantacaoId(int plantacaoId)
    {
        var ocorrencias = await _ocorrenciaService.GetByPlantacaoIdAsync(plantacaoId);

        return Ok(ocorrencias);
    }

    [HttpGet("pendentes")]
    public async Task<ActionResult<IEnumerable<OcorrenciaPlantacaoResponseDto>>> GetPendentes()
    {
        var ocorrencias = await _ocorrenciaService.GetPendentesAsync();

        return Ok(ocorrencias);
    }

    [HttpGet("criticas")]
    public async Task<ActionResult<IEnumerable<OcorrenciaPlantacaoResponseDto>>> GetCriticas()
    {
        var ocorrencias = await _ocorrenciaService.GetCriticasAsync();

        return Ok(ocorrencias);
    }

    [HttpPost]
    public async Task<ActionResult<OcorrenciaPlantacaoResponseDto>> Create([FromBody] OcorrenciaPlantacaoCreateDto dto)
    {
        var ocorrencia = await _ocorrenciaService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = ocorrencia.Id }, ocorrencia);
    }

    [HttpPatch("{id:int}/resolver")]
    public async Task<ActionResult<OcorrenciaPlantacaoResponseDto>> MarcarComoResolvida(int id)
    {
        var ocorrencia = await _ocorrenciaService.MarcarComoResolvidaAsync(id);

        if (ocorrencia is null)
            return NotFound(new { mensagem = "Ocorrência de plantação não encontrada." });

        return Ok(ocorrencia);
    }

    [HttpPatch("{id:int}/reabrir")]
    public async Task<ActionResult<OcorrenciaPlantacaoResponseDto>> Reabrir(int id)
    {
        var ocorrencia = await _ocorrenciaService.ReabrirAsync(id);

        if (ocorrencia is null)
            return NotFound(new { mensagem = "Ocorrência de plantação não encontrada." });

        return Ok(ocorrencia);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _ocorrenciaService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Ocorrência de plantação não encontrada." });

        return NoContent();
    }
}