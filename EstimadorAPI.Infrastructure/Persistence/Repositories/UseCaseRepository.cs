using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstimadorAPI.Infrastructure.Persistence.Repositories;

public class UseCaseRepository : Repository<UseCase>, IUseCaseRepository
{
    public UseCaseRepository(EstimadorDbContext context) : base(context) { }

    public async Task<UseCase?> GetUseCaseWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(u => u.Actors)
            .Include(u => u.Steps)
            .Include(u => u.Requirements)
            .Include(u => u.EstimationResult)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
    }

    public async Task<IEnumerable<UseCase>> GetProjectUseCasesAsync(int projectId)
    {
        return await _dbSet
            .Where(u => u.ProjectId == projectId && !u.IsDeleted)
            .ToListAsync();
    }

    public async Task<UseCase?> GetUseCaseByCodeAsync(string code)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Code == code && !u.IsDeleted);
    }
}
