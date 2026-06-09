using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class SafraRepository : ISafraRepository
{
    private readonly AgroVisionContext _context;

    public SafraRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Safra>> GetAllAsync()
    {
        return await _context.Safras
            .Include(s => s.Plantacao)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Safra?> GetByIdAsync(int id)
    {
        return await _context.Safras
            .Include(s => s.Plantacao)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Safra>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        return await _context.Safras
            .Where(s => s.PlantacaoId == plantacaoId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Safra safra)
    {
        await _context.Safras.AddAsync(safra);
    }

    public void Update(Safra safra)
    {
        _context.Safras.Update(safra);
    }

    public void Delete(Safra safra)
    {
        _context.Safras.Remove(safra);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Safras.AnyAsync(s => s.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}