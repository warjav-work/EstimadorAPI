using EstimadorAPI.Domain.Entities;

namespace EstimadorAPI.Domain.Interfaces.Repositories
{
    public interface IUseCaseStepRepository : IRepository<UseCaseStep>
    {
        Task<IEnumerable<UseCaseStep>> GetUseCaseStepsAsync(int useCaseId);
        Task<IEnumerable<UseCaseStep>> GetUseCaseStepsByFlowTypeAsync(int useCaseId, int flowType);
    }
}
