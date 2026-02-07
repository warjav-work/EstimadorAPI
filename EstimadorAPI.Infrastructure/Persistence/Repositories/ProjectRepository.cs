using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstimadorAPI.Infrastructure.Persistence.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(EstimadorDbContext context) : base(context) { }

    public async Task<Project?> GetProjectWithUseCasesAsync(int id)
    {
        return await _dbSet
            .Include(p => p.UseCases)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
    }

    public async Task<IEnumerable<Project>> GetProjectsWithUseCasesAsync()
    {
        return await _dbSet
            .Include(p => p.UseCases)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
    }
}
