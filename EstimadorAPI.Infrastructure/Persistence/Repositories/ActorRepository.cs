using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstimadorAPI.Infrastructure.Persistence.Repositories;

public class ActorRepository : Repository<Actor>, IActorRepository
{
    public ActorRepository(EstimadorDbContext context) : base(context) { }

    public async Task<IEnumerable<Actor>> GetUseCaseActorsAsync(int useCaseId)
    {
        return await _dbSet
            .Where(a => a.UseCaseId == useCaseId && !a.IsDeleted)
            .ToListAsync();
    }
}
