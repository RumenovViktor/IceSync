namespace IceSync.Infrastructure.Repositories;

using BL;
using BL.Domain;
using Entities.DbEntities;
using Microsoft.EntityFrameworkCore;

internal class WorkflowRepository : IWorkflowRepository
{
    private readonly IceSyncDbContext _dbContext;

    public WorkflowRepository(IceSyncDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Workflow>> Get()
    {
        var workflows = _dbContext.Workflows.Select(x => new Workflow
        {
            WorkflowId = x.WorkflowId,
            WorkflowName = x.WorkflowName,
            IsActive = x.IsActive,
            IsRunning = x.IsRunning,
            MultiExecBehaviour = x.MultiExecBehaviour
        });
        
        return await workflows.ToArrayAsync();
    }

    public async Task Insert(IEnumerable<Workflow> workflows)
    {
        var entities = workflows.Select(x => new WorkflowEntity
        {
            WorkflowName = x.WorkflowName,
            IsActive = x.IsActive,
            IsRunning = x.IsRunning,
            MultiExecBehaviour = x.MultiExecBehaviour,
            WorkflowId = x.WorkflowId
        });
        
        await _dbContext.Workflows.AddRangeAsync(entities);
    }

    public Task Update(IEnumerable<Workflow> workflows)
    {
        var ids = workflows.Select(x => x.WorkflowId);
        var workflowsDict = workflows.ToDictionary(x => x.WorkflowId, x => x);
        var entities = _dbContext.Workflows.Where(x => ids.Contains(x.WorkflowId));

        foreach (var entity in entities)
        {
            entity.IsRunning = workflowsDict[entity.WorkflowId].IsRunning;
            entity.IsActive = workflowsDict[entity.WorkflowId].IsActive;
            entity.MultiExecBehaviour = workflowsDict[entity.WorkflowId].MultiExecBehaviour;
            entity.WorkflowName = workflowsDict[entity.WorkflowId].WorkflowName;
        }

        return Task.CompletedTask;
    }

    public async Task Delete(IEnumerable<int> workflowsIds)
    {
        await _dbContext.Workflows
            .Where(x => workflowsIds.Contains(x.Id))
            .ExecuteDeleteAsync();
    }
}