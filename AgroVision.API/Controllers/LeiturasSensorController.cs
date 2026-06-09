using AgroVision.Application.DTOs.LeituraSensor;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeiturasSensorController : ControllerBase
{
    private readonly ILeituraSensorService _leituraSensorService;

    public LeiturasSensorController(ILeituraSensorService leituraSensorService)
    {
        _leituraSensorService = leituraSensorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeituraSensorResponseDto>>> GetAll()
    {
        var leituras = await _leituraSensorService.GetAllAsync();

        return Ok(leituras);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LeituraSensorResponseDto>> GetById(int id)
    {
        var leitura = await _leituraSensorService.GetByIdAsync(id);

        if (leitura is null)
            return NotFound(new { mensagem = "Leitura de sensor não encontrada." });

        return Ok(leitura);
    }

    [HttpGet("missao/{missaoDroneId:int}")]
    public async Task<ActionResult<IEnumerable<LeituraSensorResponseDto>>> GetByMissaoDroneId(int missaoDroneId)
    {
        var leituras = await _leituraSensorService.GetByMissaoDroneIdAsync(missaoDroneId);

        return Ok(leituras);
    }

    [HttpGet("fora-do-padrao")]
    public async Task<ActionResult<IEnumerable<LeituraSensorResponseDto>>> GetLeiturasForaDoPadrao()
    {
        var leituras = await _leituraSensorService.GetLeiturasForaDoPadraoAsync();

        return Ok(leituras);
    }

    [HttpPost]
    public async Task<ActionResult<LeituraSensorResponseDto>> Create([FromBody] LeituraSensorCreateDto dto)
    {
        var leitura = await _leituraSensorService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = leitura.Id }, leitura);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _leituraSensorService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Leitura de sensor não encontrada." });

        return NoContent();
    }
}