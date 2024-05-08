namespace IceSync.Infrastructure.Repositories;

using BL;
using BL.Domain;
using Clients;
using MediatR;

internal class ExternalWorkflowsRepository : IExternalWorkflowsRepository
{
    private readonly IWorkflowsClient _workflowsClient;
    
    public ExternalWorkflowsRepository(IWorkflowsClient workflowsClient)
    {
        _workflowsClient = workflowsClient;
    }
    
    public async Task<IEnumerable<Workflow>> Get()
    {
        var workflows = await _workflowsClient.Get();

        return workflows.Select(x => new Workflow
        {
            WorkflowId = x.Id,
            WorkflowName = x.Name,
            IsActive = x.IsActive,
            IsRunning = false,
            MultiExecBehaviour = x.MultiExecBehaviour
        });
    }

    public async Task<Unit> Run(int workflowId)
    {
        var result = await _workflowsClient.Run(workflowId, waitOutput:true);
        
        return Unit.Value;
    }
}