using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Domain.Enums;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class AnaliseDroneRepository : IAnaliseDroneRepository
{
    private readonly AgroVisionContext _context;

    public AnaliseDroneRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AnaliseDrone>> GetAllAsync()
    {
        return await _context.AnalisesDrone
            .Include(a => a.Plantacao)
            .Include(a => a.MissaoDrone)
            .Include(a => a.Ocorrencias)
            .Include(a => a.Recomendacoes)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<AnaliseDrone?> GetByIdAsync(int id)
    {
        return await _context.AnalisesDrone
            .Include(a => a.Plantacao)
            .Include(a => a.MissaoDrone)
            .Include(a => a.Ocorrencias)
            .Include(a => a.Recomendacoes)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<AnaliseDrone>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        return await _context.AnalisesDrone
            .Where(a => a.PlantacaoId == plantacaoId)
            .OrderByDescending(a => a.DataAnalise)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<AnaliseDrone>> GetAnalisesCriticasAsync()
    {
        return await _context.AnalisesDrone
            .Where(a =>
                a.NivelRisco == NivelRisco.ALTO ||
                a.NivelRisco == NivelRisco.CRITICO)
            .OrderByDescending(a => a.DataAnalise)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<AnaliseDrone?> GetUltimaAnaliseByPlantacaoIdAsync(int plantacaoId)
    {
        return await _context.AnalisesDrone
            .Where(a => a.PlantacaoId == plantacaoId)
            .OrderByDescending(a => a.DataAnalise)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(AnaliseDrone analiseDrone)
    {
        await _context.AnalisesDrone.AddAsync(analiseDrone);
    }

    public void Update(AnaliseDrone analiseDrone)
    {
        _context.AnalisesDrone.Update(analiseDrone);
    }

    public void Delete(AnaliseDrone analiseDrone)
    {
        _context.AnalisesDrone.Remove(analiseDrone);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.AnalisesDrone.AnyAsync(a => a.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}