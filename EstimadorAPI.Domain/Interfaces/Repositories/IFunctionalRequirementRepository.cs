using EstimadorAPI.Domain.Entities;

namespace EstimadorAPI.Domain.Interfaces.Repositories
{
    public interface IFunctionalRequirementRepository : IRepository<FunctionalRequirement>
    {
        Task<IEnumerable<FunctionalRequirement>> GetUseCaseRequirementsAsync(int useCaseId);
        Task<FunctionalRequirement?> GetRequirementWithDependenciesAsync(int id);
        Task<IEnumerable<FunctionalRequirement>> GetComplexRequirementsAsync(int useCaseId);
    }
}
