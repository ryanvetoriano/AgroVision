using AgroVision.Application.DTOs.MissaoDrone;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissoesDroneController : ControllerBase
{
    private readonly IMissaoDroneService _missaoDroneService;

    public MissoesDroneController(IMissaoDroneService missaoDroneService)
    {
        _missaoDroneService = missaoDroneService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MissaoDroneResponseDto>>> GetAll()
    {
        var missoes = await _missaoDroneService.GetAllAsync();

        return Ok(missoes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MissaoDroneResponseDto>> GetById(int id)
    {
        var missao = await _missaoDroneService.GetByIdAsync(id);

        if (missao is null)
            return NotFound(new { mensagem = "Missão de drone não encontrada." });

        return Ok(missao);
    }

    [HttpGet("drone/{droneId:int}")]
    public async Task<ActionResult<IEnumerable<MissaoDroneResponseDto>>> GetByDroneId(int droneId)
    {
        var missoes = await _missaoDroneService.GetByDroneIdAsync(droneId);

        return Ok(missoes);
    }

    [HttpGet("plantacao/{plantacaoId:int}")]
    public async Task<ActionResult<IEnumerable<MissaoDroneResponseDto>>> GetByPlantacaoId(int plantacaoId)
    {
        var missoes = await _missaoDroneService.GetByPlantacaoIdAsync(plantacaoId);

        return Ok(missoes);
    }

    [HttpGet("em-andamento")]
    public async Task<ActionResult<IEnumerable<MissaoDroneResponseDto>>> GetMissoesEmAndamento()
    {
        var missoes = await _missaoDroneService.GetMissoesEmAndamentoAsync();

        return Ok(missoes);
    }

    [HttpPost]
    public async Task<ActionResult<MissaoDroneResponseDto>> Create([FromBody] MissaoDroneCreateDto dto)
    {
        var missao = await _missaoDroneService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = missao.Id }, missao);
    }

    [HttpPatch("{id:int}/iniciar")]
    public async Task<ActionResult<MissaoDroneResponseDto>> IniciarMissao(int id)
    {
        var missao = await _missaoDroneService.IniciarMissaoAsync(id);

        if (missao is null)
            return NotFound(new { mensagem = "Missão de drone não encontrada." });

        return Ok(missao);
    }

    [HttpPatch("{id:int}/finalizar")]
    public async Task<ActionResult<MissaoDroneResponseDto>> FinalizarMissao(int id)
    {
        var missao = await _missaoDroneService.FinalizarMissaoAsync(id);

        if (missao is null)
            return NotFound(new { mensagem = "Missão de drone não encontrada." });

        return Ok(missao);
    }

    [HttpPatch("{id:int}/cancelar")]
    public async Task<ActionResult<MissaoDroneResponseDto>> CancelarMissao(int id)
    {
        var missao = await _missaoDroneService.CancelarMissaoAsync(id);

        if (missao is null)
            return NotFound(new { mensagem = "Missão de drone não encontrada." });

        return Ok(missao);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _missaoDroneService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Missão de drone não encontrada." });

        return NoContent();
    }
}