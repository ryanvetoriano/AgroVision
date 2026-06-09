using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class InsumoRepository : IInsumoRepository
{
    private readonly AgroVisionContext _context;

    public InsumoRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Insumo>> GetAllAsync()
    {
        return await _context.Insumos
            .Include(i => i.Plantacao)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Insumo?> GetByIdAsync(int id)
    {
        return await _context.Insumos
            .Include(i => i.Plantacao)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Insumo>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        return await _context.Insumos
            .Where(i => i.PlantacaoId == plantacaoId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Insumo>> GetComEstoqueBaixoAsync()
    {
        return await _context.Insumos
            .Where(i => i.QuantidadeEstoque <= i.EstoqueMinimo)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Insumo insumo)
    {
        await _context.Insumos.AddAsync(insumo);
    }

    public void Update(Insumo insumo)
    {
        _context.Insumos.Update(insumo);
    }

    public void Delete(Insumo insumo)
    {
        _context.Insumos.Remove(insumo);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Insumos.AnyAsync(i => i.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}