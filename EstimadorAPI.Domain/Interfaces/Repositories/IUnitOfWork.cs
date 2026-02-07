namespace EstimadorAPI.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }
        IUseCaseRepository UseCases { get; }
        IActorRepository Actors { get; }
        IUseCaseStepRepository UseCaseSteps { get; }
        IFunctionalRequirementRepository FunctionalRequirements { get; }
        IRequirementDependencyRepository RequirementDependencies { get; }
        IEstimationResultRepository EstimationResults { get; }

        Task<int> SaveChangesAsync();
        Task<bool> BeginTransactionAsync();
        Task<bool> CommitTransactionAsync();
        Task<bool> RollbackTransactionAsync();
    }
}
