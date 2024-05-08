namespace IceSync.BL;

public interface IUnitOfWork
{
    IWorkflowRepository Workflows { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken);
}