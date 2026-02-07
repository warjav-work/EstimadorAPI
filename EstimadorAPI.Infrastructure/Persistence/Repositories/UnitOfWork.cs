using EstimadorAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EstimadorAPI.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EstimadorDbContext _context;
    private IProjectRepository? _projectRepository;
    private IUseCaseRepository? _useCaseRepository;
    private IActorRepository? _actorRepository;
    private IUseCaseStepRepository? _useCaseStepRepository;
    private IFunctionalRequirementRepository? _functionalRequirementRepository;
    private IRequirementDependencyRepository? _requirementDependencyRepository;
    private IEstimationResultRepository? _estimationResultRepository;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(EstimadorDbContext context)
    {
        _context = context;
    }

    public IProjectRepository Projects => _projectRepository ??= new ProjectRepository(_context);
    public IUseCaseRepository UseCases => _useCaseRepository ??= new UseCaseRepository(_context);
    public IActorRepository Actors => _actorRepository ??= new ActorRepository(_context);
    public IUseCaseStepRepository UseCaseSteps => _useCaseStepRepository ??= new UseCaseStepRepository(_context);
    public IFunctionalRequirementRepository FunctionalRequirements =>
        _functionalRequirementRepository ??= new FunctionalRequirementRepository(_context);
    public IRequirementDependencyRepository RequirementDependencies =>
        _requirementDependencyRepository ??= new RequirementDependencyRepository(_context);
    public IEstimationResultRepository EstimationResults =>
        _estimationResultRepository ??= new EstimationResultRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return true;
    }

    public async Task<bool> CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction?.CommitAsync()!;
            return true;
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task<bool> RollbackTransactionAsync()
    {
        try
        {
            await _transaction?.RollbackAsync()!;
            return true;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
