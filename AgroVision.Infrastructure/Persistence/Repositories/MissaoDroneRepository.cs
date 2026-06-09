using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Domain.Enums;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class MissaoDroneRepository : IMissaoDroneRepository
{
    private readonly AgroVisionContext _context;

    public MissaoDroneRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MissaoDrone>> GetAllAsync()
    {
        return await _context.MissoesDrone
            .Include(m => m.Drone)
            .Include(m => m.Plantacao)
            .Include(m => m.LeiturasSensor)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<MissaoDrone?> GetByIdAsync(int id)
    {
        return await _context.MissoesDrone
            .Include(m => m.Drone)
            .Include(m => m.Plantacao)
            .Include(m => m.LeiturasSensor)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<MissaoDrone>> GetByDroneIdAsync(int droneId)
    {
        return await _context.MissoesDrone
            .Where(m => m.DroneId == droneId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<MissaoDrone>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        return await _context.MissoesDrone
            .Where(m => m.PlantacaoId == plantacaoId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<MissaoDrone>> GetMissoesEmAndamentoAsync()
    {
        return await _context.MissoesDrone
            .Where(m => m.Status == StatusMissaoDrone.EM_ANDAMENTO)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(MissaoDrone missaoDrone)
    {
        await _context.MissoesDrone.AddAsync(missaoDrone);
    }

    public void Update(MissaoDrone missaoDrone)
    {
        _context.MissoesDrone.Update(missaoDrone);
    }

    public void Delete(MissaoDrone missaoDrone)
    {
        _context.MissoesDrone.Remove(missaoDrone);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.MissoesDrone.AnyAsync(m => m.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}