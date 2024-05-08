namespace IceSync.BL.Queries.GetWorkflows;

using MediatR;

public class GetWorkflowsQueryHandler : IRequestHandler<GetWorkflowsQuery, GetWorkflowsQueryResult>
{
    private readonly IExternalWorkflowsRepository _externalWorkflowsRepository;

    public GetWorkflowsQueryHandler(IExternalWorkflowsRepository externalWorkflowsRepository)
    {
        _externalWorkflowsRepository = externalWorkflowsRepository;
    }

    public async Task<GetWorkflowsQueryResult> Handle(GetWorkflowsQuery request, CancellationToken cancellationToken)
    {
        var allWorkflows = await _externalWorkflowsRepository.Get();
        return new GetWorkflowsQueryResult(allWorkflows);
    }
}