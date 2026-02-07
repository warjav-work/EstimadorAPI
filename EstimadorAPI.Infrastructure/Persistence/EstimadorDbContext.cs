using EstimadorAPI.Domain.Common;
using EstimadorAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstimadorAPI.Infrastructure.Persistence;

public class EstimadorDbContext(DbContextOptions<EstimadorDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<UseCase> UseCases => Set<UseCase>();
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<UseCaseStep> UseCaseSteps => Set<UseCaseStep>();
    public DbSet<FunctionalRequirement> FunctionalRequirements => Set<FunctionalRequirement>();
    public DbSet<RequirementDependency> RequirementDependencies => Set<RequirementDependency>();
    public DbSet<EstimationResult> EstimationResults => Set<EstimationResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Project configuration
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.ClientName).HasMaxLength(100);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasMany(e => e.UseCases).WithOne(e => e.Project).HasForeignKey(e => e.ProjectId).OnDelete(DeleteBehavior.Cascade);
        });

        // UseCase configuration
        modelBuilder.Entity<UseCase>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Preconditions).HasMaxLength(500);
            entity.Property(e => e.Postconditions).HasMaxLength(500);
            entity.HasIndex(e => new { e.ProjectId, e.Code }).IsUnique();
            entity.HasMany(e => e.Actors).WithOne(e => e.UseCase).HasForeignKey(e => e.UseCaseId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e => e.Steps).WithOne(e => e.UseCase).HasForeignKey(e => e.UseCaseId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e => e.Requirements).WithOne(e => e.UseCase).HasForeignKey(e => e.UseCaseId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.EstimationResult).WithOne(e => e.UseCase).HasForeignKey<EstimationResult>(e => e.UseCaseId).OnDelete(DeleteBehavior.Cascade);
        });

        // Actor configuration
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(200);
        });

        // UseCaseStep configuration
        modelBuilder.Entity<UseCaseStep>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(300);
            entity.HasOne(e => e.ResponsibleActor).WithMany().HasForeignKey(e => e.ResponsibleActorId).OnDelete(DeleteBehavior.SetNull);
            entity.HasMany(e => e.Requirements).WithOne(e => e.UseCaseStep).HasForeignKey(e => e.UseCaseStepId).OnDelete(DeleteBehavior.SetNull);
        });

        // FunctionalRequirement configuration
        modelBuilder.Entity<FunctionalRequirement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.HasIndex(e => new { e.UseCaseId, e.Code }).IsUnique();
            entity.HasMany(e => e.Dependencies).WithOne(e => e.Requirement).HasForeignKey(e => e.RequirementId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e => e.DependentOn).WithOne(e => e.DependsOnRequirement).HasForeignKey(e => e.DependsOnRequirementId).OnDelete(DeleteBehavior.NoAction);
        });

        // RequirementDependency configuration
        modelBuilder.Entity<RequirementDependency>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Requirement).WithMany(e => e.Dependencies).HasForeignKey(e => e.RequirementId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.DependsOnRequirement).WithMany(e => e.DependentOn).HasForeignKey(e => e.DependsOnRequirementId).OnDelete(DeleteBehavior.NoAction);
            entity.HasIndex(e => new { e.RequirementId, e.DependsOnRequirementId }).IsUnique();
        });

        // EstimationResult configuration
        modelBuilder.Entity<EstimationResult>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UseCaseId).IsUnique();
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries().Where(e => e.Entity is Entity);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                ((Entity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
