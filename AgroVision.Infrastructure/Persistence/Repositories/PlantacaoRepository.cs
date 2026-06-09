using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class PlantacaoRepository : IPlantacaoRepository
{
    private readonly AgroVisionContext _context;

    public PlantacaoRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Plantacao>> GetAllAsync()
    {
        return await _context.Plantacoes
            .Include(p => p.Usuario)
            .Include(p => p.Safras)
            .Include(p => p.Insumos)
            .Include(p => p.AnalisesDrone)
            .Include(p => p.MissoesDrone)
            .Include(p => p.Ocorrencias)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Plantacao?> GetByIdAsync(int id)
    {
        return await _context.Plantacoes
            .Include(p => p.Usuario)
            .Include(p => p.Safras)
            .Include(p => p.Insumos)
            .Include(p => p.AnalisesDrone)
            .Include(p => p.MissoesDrone)
            .Include(p => p.Ocorrencias)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Plantacao>> GetByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Plantacoes
            .Where(p => p.UsuarioId == usuarioId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Plantacao>> GetAtivasAsync()
    {
        return await _context.Plantacoes
            .Where(p => p.Ativa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Plantacao plantacao)
    {
        await _context.Plantacoes.AddAsync(plantacao);
    }

    public void Update(Plantacao plantacao)
    {
        _context.Plantacoes.Update(plantacao);
    }

    public void Delete(Plantacao plantacao)
    {
        _context.Plantacoes.Remove(plantacao);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Plantacoes.AnyAsync(p => p.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}