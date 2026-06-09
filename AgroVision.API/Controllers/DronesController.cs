using AgroVision.Application.DTOs.Drone;
using AgroVision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DronesController : ControllerBase
{
    private readonly IDroneService _droneService;

    public DronesController(IDroneService droneService)
    {
        _droneService = droneService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DroneResponseDto>>> GetAll()
    {
        var drones = await _droneService.GetAllAsync();

        return Ok(drones);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DroneResponseDto>> GetById(int id)
    {
        var drone = await _droneService.GetByIdAsync(id);

        if (drone is null)
            return NotFound(new { mensagem = "Drone não encontrado." });

        return Ok(drone);
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<DroneResponseDto>>> GetAtivos()
    {
        var drones = await _droneService.GetAtivosAsync();

        return Ok(drones);
    }

    [HttpPost]
    public async Task<ActionResult<DroneResponseDto>> Create([FromBody] DroneCreateDto dto)
    {
        var drone = await _droneService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = drone.Id }, drone);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DroneResponseDto>> Update(
        int id,
        [FromBody] DroneUpdateDto dto)
    {
        var drone = await _droneService.UpdateAsync(id, dto);

        if (drone is null)
            return NotFound(new { mensagem = "Drone não encontrado." });

        return Ok(drone);
    }

    [HttpPatch("{id:int}/ativar")]
    public async Task<ActionResult> Ativar(int id)
    {
        var atualizado = await _droneService.AtivarAsync(id);

        if (!atualizado)
            return NotFound(new { mensagem = "Drone não encontrado." });

        return NoContent();
    }

    [HttpPatch("{id:int}/desativar")]
    public async Task<ActionResult> Desativar(int id)
    {
        var atualizado = await _droneService.DesativarAsync(id);

        if (!atualizado)
            return NotFound(new { mensagem = "Drone não encontrado." });

        return NoContent();
    }

    [HttpPost("{id:int}/verificar-autonomia")]
    public async Task<ActionResult<object>> VerificarAutonomia(
        int id,
        [FromBody] DroneAutonomiaDto dto)
    {
        var possuiAutonomia = await _droneService.VerificarAutonomiaAsync(id, dto);

        if (possuiAutonomia is null)
            return NotFound(new { mensagem = "Drone não encontrado." });

        return Ok(new
        {
            droneId = id,
            duracaoEstimadaMinutos = dto.DuracaoEstimadaMinutos,
            possuiAutonomia
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var removido = await _droneService.DeleteAsync(id);

        if (!removido)
            return NotFound(new { mensagem = "Drone não encontrado." });

        return NoContent();
    }
}