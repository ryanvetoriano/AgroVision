using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Domain.Enums;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class RecomendacaoAgronomicaRepository : IRecomendacaoAgronomicaRepository
{
    private readonly AgroVisionContext _context;

    public RecomendacaoAgronomicaRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RecomendacaoAgronomica>> GetAllAsync()
    {
        return await _context.RecomendacoesAgronomicas
            .Include(r => r.AnaliseDrone)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<RecomendacaoAgronomica?> GetByIdAsync(int id)
    {
        return await _context.RecomendacoesAgronomicas
            .Include(r => r.AnaliseDrone)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<RecomendacaoAgronomica>> GetByAnaliseDroneIdAsync(int analiseDroneId)
    {
        return await _context.RecomendacoesAgronomicas
            .Where(r => r.AnaliseDroneId == analiseDroneId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<RecomendacaoAgronomica>> GetPendentesAsync()
    {
        return await _context.RecomendacoesAgronomicas
            .Where(r => !r.Concluida)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<RecomendacaoAgronomica>> GetUrgentesAsync()
    {
        return await _context.RecomendacoesAgronomicas
            .Where(r =>
                !r.Concluida &&
                (r.Prioridade == PrioridadeRecomendacao.ALTA ||
                 r.Prioridade == PrioridadeRecomendacao.CRITICA))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(RecomendacaoAgronomica recomendacaoAgronomica)
    {
        await _context.RecomendacoesAgronomicas.AddAsync(recomendacaoAgronomica);
    }

    public void Update(RecomendacaoAgronomica recomendacaoAgronomica)
    {
        _context.RecomendacoesAgronomicas.Update(recomendacaoAgronomica);
    }

    public void Delete(RecomendacaoAgronomica recomendacaoAgronomica)
    {
        _context.RecomendacoesAgronomicas.Remove(recomendacaoAgronomica);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.RecomendacoesAgronomicas.AnyAsync(r => r.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}