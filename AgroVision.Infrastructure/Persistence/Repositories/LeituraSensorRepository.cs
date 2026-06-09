using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class LeituraSensorRepository : ILeituraSensorRepository
{
    private readonly AgroVisionContext _context;

    public LeituraSensorRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LeituraSensor>> GetAllAsync()
    {
        return await _context.LeiturasSensor
            .Include(l => l.MissaoDrone)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<LeituraSensor?> GetByIdAsync(int id)
    {
        return await _context.LeiturasSensor
            .Include(l => l.MissaoDrone)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IEnumerable<LeituraSensor>> GetByMissaoDroneIdAsync(int missaoDroneId)
    {
        return await _context.LeiturasSensor
            .Where(l => l.MissaoDroneId == missaoDroneId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<LeituraSensor>> GetLeiturasForaDoPadraoAsync()
    {
        var leituras = await _context.LeiturasSensor
            .AsNoTracking()
            .ToListAsync();

        return leituras.Where(l => l.EstaForaDoPadrao());
    }

    public async Task AddAsync(LeituraSensor leituraSensor)
    {
        await _context.LeiturasSensor.AddAsync(leituraSensor);
    }

    public void Update(LeituraSensor leituraSensor)
    {
        _context.LeiturasSensor.Update(leituraSensor);
    }

    public void Delete(LeituraSensor leituraSensor)
    {
        _context.LeiturasSensor.Remove(leituraSensor);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.LeiturasSensor.AnyAsync(l => l.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}