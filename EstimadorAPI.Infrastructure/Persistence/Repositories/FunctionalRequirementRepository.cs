using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstimadorAPI.Infrastructure.Persistence.Repositories;

public class FunctionalRequirementRepository : Repository<FunctionalRequirement>, IFunctionalRequirementRepository
{
    public FunctionalRequirementRepository(EstimadorDbContext context) : base(context) { }

    public async Task<IEnumerable<FunctionalRequirement>> GetUseCaseRequirementsAsync(int useCaseId)
    {
        return await _dbSet
            .Where(r => r.UseCaseId == useCaseId && !r.IsDeleted)
            .ToListAsync();
    }

    public async Task<FunctionalRequirement?> GetRequirementWithDependenciesAsync(int id)
    {
        return await _dbSet
            .Include(r => r.Dependencies)
            .Include(r => r.DependentOn)
            .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
    }

    public async Task<IEnumerable<FunctionalRequirement>> GetComplexRequirementsAsync(int useCaseId)
    {
        return await _dbSet
            .Where(r => r.UseCaseId == useCaseId && (int)r.Complexity == 3 && !r.IsDeleted)
            .ToListAsync();
    }
}
