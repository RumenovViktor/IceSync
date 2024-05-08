namespace IceSync.Infrastructure;

using BL;

public class UnitOfWork : IUnitOfWork
{
    private readonly IceSyncDbContext _dbContext;

    public UnitOfWork(IceSyncDbContext dbContext, IWorkflowRepository workflowRepository)
    {
        _dbContext = dbContext;
        Workflows = workflowRepository;
    }

    public IWorkflowRepository Workflows { get; }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _dbContext.SaveChangesAsync(cancellationToken);
}