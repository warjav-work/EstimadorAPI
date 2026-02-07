using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstimadorAPI.Infrastructure.Persistence.Repositories;

public class RequirementDependencyRepository : Repository<RequirementDependency>, IRequirementDependencyRepository
{
    public RequirementDependencyRepository(EstimadorDbContext context) : base(context) { }

    public async Task<IEnumerable<RequirementDependency>> GetRequirementDependenciesAsync(int requirementId)
    {
        return await _dbSet
            .Where(d => d.RequirementId == requirementId && !d.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<RequirementDependency>> GetDependentOnAsync(int requirementId)
    {
        return await _dbSet
            .Where(d => d.DependsOnRequirementId == requirementId && !d.IsDeleted)
            .ToListAsync();
    }
}
