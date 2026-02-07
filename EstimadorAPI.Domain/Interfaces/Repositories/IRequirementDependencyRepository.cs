using EstimadorAPI.Domain.Entities;

namespace EstimadorAPI.Domain.Interfaces.Repositories
{
    public interface IRequirementDependencyRepository : IRepository<RequirementDependency>
    {
        Task<IEnumerable<RequirementDependency>> GetRequirementDependenciesAsync(int requirementId);
        Task<IEnumerable<RequirementDependency>> GetDependentOnAsync(int requirementId);
    }
}
