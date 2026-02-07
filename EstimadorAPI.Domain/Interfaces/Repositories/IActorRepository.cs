using EstimadorAPI.Domain.Entities;

namespace EstimadorAPI.Domain.Interfaces.Repositories
{
    public interface IActorRepository : IRepository<Actor>
    {
        Task<IEnumerable<Actor>> GetUseCaseActorsAsync(int useCaseId);
    }
}
