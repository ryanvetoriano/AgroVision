using AgroVision.Application.DTOs.Insumo;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsumosController : ControllerBase
{
    private readonly IInsumoService _insumoService;

    public InsumosController(IInsumoService insumoService)
    {
        _insumoService = insumoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsumoResponseDto>>> GetAll()
    {
        var insumos = await _insumoService.GetAllAsync();

        return Ok(insumos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InsumoResponseDto>> GetById(int id)
    {
        var insumo = await _insumoService.GetByIdAsync(id);

        if (insumo is null)
            return NotFound(new { mensagem = "Insumo não encontrado." });

        return Ok(insumo);
    }

    [HttpGet("plantacao/{plantacaoId:int}")]
    public async Task<ActionResult<IEnumerable<InsumoResponseDto>>> GetByPlantacaoId(int plantacaoId)
    {
        var insumos = await _insumoService.GetByPlantacaoIdAsync(plantacaoId);

        return Ok(insumos);
    }

    [HttpGet("estoque-baixo")]
    public async Task<ActionResult<IEnumerable<InsumoResponseDto>>> GetComEstoqueBaixo()
    {
        var insumos = await _insumoService.GetComEstoqueBaixoAsync();

        return Ok(insumos);
    }

    [HttpPost]
    public async Task<ActionResult<InsumoResponseDto>> Create([FromBody] InsumoCreateDto dto)
    {
        var insumo = await _insumoService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = insumo.Id }, insumo);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<InsumoResponseDto>> Update(
        int id,
        [FromBody] InsumoUpdateDto dto)
    {
        var insumo = await _insumoService.UpdateAsync(id, dto);

        if (insumo is null)
            return NotFound(new { mensagem = "Insumo não encontrado." });

        return Ok(insumo);
    }

    [HttpPatch("{id:int}/registrar-aplicacao")]
    public async Task<ActionResult<InsumoResponseDto>> RegistrarAplicacao(
        int id,
        [FromBody] InsumoAplicacaoDto dto)
    {
        var insumo = await _insumoService.RegistrarAplicacaoAsync(id, dto);

        if (insumo is null)
            return NotFound(new { mensagem = "Insumo não encontrado." });

        return Ok(insumo);
    }

    [HttpPatch("{id:int}/repor-estoque")]
    public async Task<ActionResult<InsumoResponseDto>> ReporEstoque(
        int id,
        [FromBody] InsumoReposicaoDto dto)
    {
        var insumo = await _insumoService.ReporEstoqueAsync(id, dto);

        if (insumo is null)
            return NotFound(new { mensagem = "Insumo não encontrado." });

        return Ok(insumo);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _insumoService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Insumo não encontrado." });

        return NoContent();
    }
}