namespace IceSync.BL;

using Domain;
using MediatR;

public interface IExternalWorkflowsRepository
{
    Task<IEnumerable<Workflow>> Get();
    
    Task<Unit> Run(int workflowId);
}