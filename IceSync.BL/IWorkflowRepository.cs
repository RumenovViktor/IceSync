namespace IceSync.BL;

using Domain;

public interface IWorkflowRepository
{
    Task<IEnumerable<Workflow>> Get();
    
    Task Insert(IEnumerable<Workflow> workflows);
    
    Task Update(IEnumerable<Workflow> workflows);
    
    Task Delete(IEnumerable<int> workflowsIds);
}