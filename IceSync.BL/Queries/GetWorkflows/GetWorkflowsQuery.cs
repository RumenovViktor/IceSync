namespace IceSync.BL.Queries.GetWorkflows;

using MediatR;

public record GetWorkflowsQuery() : IRequest<GetWorkflowsQueryResult>;