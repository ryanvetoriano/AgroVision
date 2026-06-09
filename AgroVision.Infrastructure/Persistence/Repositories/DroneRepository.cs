using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class DroneRepository : IDroneRepository
{
    private readonly AgroVisionContext _context;

    public DroneRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Drone>> GetAllAsync()
    {
        return await _context.Drones
            .Include(d => d.Missoes)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Drone?> GetByIdAsync(int id)
    {
        return await _context.Drones
            .Include(d => d.Missoes)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Drone?> GetByCodigoIdentificacaoAsync(string codigoIdentificacao)
    {
        return await _context.Drones
            .FirstOrDefaultAsync(d => d.CodigoIdentificacao == codigoIdentificacao);
    }

    public async Task<IEnumerable<Drone>> GetAtivosAsync()
    {
        return await _context.Drones
            .Where(d => d.Ativo)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Drone drone)
    {
        await _context.Drones.AddAsync(drone);
    }

    public void Update(Drone drone)
    {
        _context.Drones.Update(drone);
    }

    public void Delete(Drone drone)
    {
        _context.Drones.Remove(drone);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Drones.AnyAsync(d => d.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}