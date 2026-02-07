using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstimadorAPI.Infrastructure.Persistence.Repositories;

public class UseCaseStepRepository : Repository<UseCaseStep>, IUseCaseStepRepository
{
    public UseCaseStepRepository(EstimadorDbContext context) : base(context) { }

    public async Task<IEnumerable<UseCaseStep>> GetUseCaseStepsAsync(int useCaseId)
    {
        return await _dbSet
            .Where(s => s.UseCaseId == useCaseId && !s.IsDeleted)
            .OrderBy(s => s.StepNumber)
            .ToListAsync();
    }

    public async Task<IEnumerable<UseCaseStep>> GetUseCaseStepsByFlowTypeAsync(int useCaseId, int flowType)
    {
        return await _dbSet
            .Where(s => s.UseCaseId == useCaseId && (int)s.FlowType == flowType && !s.IsDeleted)
            .OrderBy(s => s.StepNumber)
            .ToListAsync();
    }
}
