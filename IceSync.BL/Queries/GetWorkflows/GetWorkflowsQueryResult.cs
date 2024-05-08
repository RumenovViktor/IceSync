namespace IceSync.BL.Queries.GetWorkflows;

using Domain;

public record GetWorkflowsQueryResult(IEnumerable<Workflow> Workflows);