using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface ILogErroRepository
{
    Task<IEnumerable<LogErro>> GetAllAsync();
    Task<LogErro?> GetByIdAsync(int id);
    Task AddAsync(LogErro logErro);
    Task SaveChangesAsync();
}