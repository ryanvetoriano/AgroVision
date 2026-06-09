using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Domain.Enums;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class OcorrenciaPlantacaoRepository : IOcorrenciaPlantacaoRepository
{
    private readonly AgroVisionContext _context;

    public OcorrenciaPlantacaoRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OcorrenciaPlantacao>> GetAllAsync()
    {
        return await _context.OcorrenciasPlantacao
            .Include(o => o.Plantacao)
            .Include(o => o.AnaliseDrone)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<OcorrenciaPlantacao?> GetByIdAsync(int id)
    {
        return await _context.OcorrenciasPlantacao
            .Include(o => o.Plantacao)
            .Include(o => o.AnaliseDrone)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<OcorrenciaPlantacao>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        return await _context.OcorrenciasPlantacao
            .Where(o => o.PlantacaoId == plantacaoId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<OcorrenciaPlantacao>> GetPendentesAsync()
    {
        return await _context.OcorrenciasPlantacao
            .Where(o => !o.Resolvida)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<OcorrenciaPlantacao>> GetCriticasAsync()
    {
        return await _context.OcorrenciasPlantacao
            .Where(o =>
                o.NivelRisco == NivelRisco.ALTO ||
                o.NivelRisco == NivelRisco.CRITICO)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(OcorrenciaPlantacao ocorrenciaPlantacao)
    {
        await _context.OcorrenciasPlantacao.AddAsync(ocorrenciaPlantacao);
    }

    public void Update(OcorrenciaPlantacao ocorrenciaPlantacao)
    {
        _context.OcorrenciasPlantacao.Update(ocorrenciaPlantacao);
    }

    public void Delete(OcorrenciaPlantacao ocorrenciaPlantacao)
    {
        _context.OcorrenciasPlantacao.Remove(ocorrenciaPlantacao);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.OcorrenciasPlantacao.AnyAsync(o => o.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}