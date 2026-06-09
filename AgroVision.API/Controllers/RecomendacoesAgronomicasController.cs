using AgroVision.Application.DTOs.RecomendacaoAgronomica;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecomendacoesAgronomicasController : ControllerBase
{
    private readonly IRecomendacaoAgronomicaService _recomendacaoService;

    public RecomendacoesAgronomicasController(IRecomendacaoAgronomicaService recomendacaoService)
    {
        _recomendacaoService = recomendacaoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecomendacaoAgronomicaResponseDto>>> GetAll()
    {
        var recomendacoes = await _recomendacaoService.GetAllAsync();

        return Ok(recomendacoes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RecomendacaoAgronomicaResponseDto>> GetById(int id)
    {
        var recomendacao = await _recomendacaoService.GetByIdAsync(id);

        if (recomendacao is null)
            return NotFound(new { mensagem = "Recomendação agronômica não encontrada." });

        return Ok(recomendacao);
    }

    [HttpGet("analise/{analiseDroneId:int}")]
    public async Task<ActionResult<IEnumerable<RecomendacaoAgronomicaResponseDto>>> GetByAnaliseDroneId(int analiseDroneId)
    {
        var recomendacoes = await _recomendacaoService.GetByAnaliseDroneIdAsync(analiseDroneId);

        return Ok(recomendacoes);
    }

    [HttpGet("pendentes")]
    public async Task<ActionResult<IEnumerable<RecomendacaoAgronomicaResponseDto>>> GetPendentes()
    {
        var recomendacoes = await _recomendacaoService.GetPendentesAsync();

        return Ok(recomendacoes);
    }

    [HttpGet("urgentes")]
    public async Task<ActionResult<IEnumerable<RecomendacaoAgronomicaResponseDto>>> GetUrgentes()
    {
        var recomendacoes = await _recomendacaoService.GetUrgentesAsync();

        return Ok(recomendacoes);
    }

    [HttpPost]
    public async Task<ActionResult<RecomendacaoAgronomicaResponseDto>> Create([FromBody] RecomendacaoAgronomicaCreateDto dto)
    {
        var recomendacao = await _recomendacaoService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = recomendacao.Id }, recomendacao);
    }

    [HttpPatch("{id:int}/concluir")]
    public async Task<ActionResult<RecomendacaoAgronomicaResponseDto>> MarcarComoConcluida(int id)
    {
        var recomendacao = await _recomendacaoService.MarcarComoConcluidaAsync(id);

        if (recomendacao is null)
            return NotFound(new { mensagem = "Recomendação agronômica não encontrada." });

        return Ok(recomendacao);
    }

    [HttpPatch("{id:int}/reabrir")]
    public async Task<ActionResult<RecomendacaoAgronomicaResponseDto>> Reabrir(int id)
    {
        var recomendacao = await _recomendacaoService.ReabrirAsync(id);

        if (recomendacao is null)
            return NotFound(new { mensagem = "Recomendação agronômica não encontrada." });

        return Ok(recomendacao);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _recomendacaoService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Recomendação agronômica não encontrada." });

        return NoContent();
    }
}