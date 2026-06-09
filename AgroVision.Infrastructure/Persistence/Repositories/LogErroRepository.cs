using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Domain.Entities;
using AgroVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence.Repositories;

public class LogErroRepository : ILogErroRepository
{
    private readonly AgroVisionContext _context;

    public LogErroRepository(AgroVisionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LogErro>> GetAllAsync()
    {
        return await _context.LogsErro
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<LogErro?> GetByIdAsync(int id)
    {
        return await _context.LogsErro
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task AddAsync(LogErro logErro)
    {
        await _context.LogsErro.AddAsync(logErro);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}