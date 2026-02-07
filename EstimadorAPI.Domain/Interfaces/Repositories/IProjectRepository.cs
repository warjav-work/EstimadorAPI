using EstimadorAPI.Domain.Entities;

namespace EstimadorAPI.Domain.Interfaces.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project?> GetProjectWithUseCasesAsync(int id);
        Task<IEnumerable<Project>> GetProjectsWithUseCasesAsync();
    }
}
