using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstimadorAPI.Infrastructure.Persistence.Repositories;

public class EstimationResultRepository : Repository<EstimationResult>, IEstimationResultRepository
{
    public EstimationResultRepository(EstimadorDbContext context) : base(context) { }

    public async Task<EstimationResult?> GetByUseCaseIdAsync(int useCaseId)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.UseCaseId == useCaseId && !e.IsDeleted);
    }

    public async Task<IEnumerable<EstimationResult>> GetProjectEstimationsAsync(int projectId)
    {
        return await _dbSet
            .Where(e => e.UseCase!.ProjectId == projectId && !e.IsDeleted)
            .ToListAsync();
    }
}

