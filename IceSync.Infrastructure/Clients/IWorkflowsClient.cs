namespace IceSync.Infrastructure.Clients;

using Authorization;
using Entities.ClientEntities;
using Refit;

internal interface IWorkflowsClient
{
    [Headers("Authorization: Bearer")]
    [Get("/workflows")]
    Task<IEnumerable<ExternalWorkflowEntity>> Get();
    
    [Post("/authenticate")]
    Task<string> Authenticate([Body] AuthSecrets body);
    
    [Post("/v2/authenticate")]
    Task<string> AuthenticateV2([Body] AuthSecrets body);
    
    [Post("/workflows/{workflowId}/run?waitOutput={waitOutput}")]
    Task<ApiResponse<object>> Run(int workflowId, bool waitOutput);
}