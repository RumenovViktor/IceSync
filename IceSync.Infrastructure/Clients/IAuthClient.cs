namespace IceSync.Infrastructure.Clients;

using Authorization;
using Entities.ClientEntities;
using Refit;

internal interface IAuthClient
{
    
    [Post("/authenticate")]
    Task<string> Authenticate([Body] AuthSecrets body);
    
    [Post("/v2/authenticate")]
    Task<string> AuthenticateV2([Body] AuthSecrets body);
}