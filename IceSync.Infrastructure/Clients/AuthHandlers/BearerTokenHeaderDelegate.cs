namespace IceSync.Infrastructure.Clients.AuthHandlers;

using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Authorization;
using BL.Configurations;
using Microsoft.Extensions.Options;

internal sealed class BearerTokenHeaderDelegate : DelegatingHandler
{
    private readonly IAuthClient _authClient;
    private readonly IOptionsMonitor<AuthorizationSettings> _authorizationConfig;
    private readonly IAccessToken _accessToken;

    public BearerTokenHeaderDelegate(
        IAuthClient authClient, 
        IOptionsMonitor<AuthorizationSettings> authorizationConfig, 
        IAccessToken accessToken)
    {
        _authClient = authClient;
        _authorizationConfig = authorizationConfig;
        _accessToken = accessToken;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!IsTokenValid(_accessToken.Token))
        {
            var token = await _authClient.Authenticate(new AuthSecrets
            {
                ApiCompanyId = _authorizationConfig.CurrentValue.ApiCompanyId,
                ApiUserId = _authorizationConfig.CurrentValue.ApiUserId,
                ApiUserSecret = _authorizationConfig.CurrentValue.ApiUserSecret
            });

            _accessToken.SetToken(token);
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken.Token);
        
        return await base.SendAsync(request, cancellationToken);
    }
    
    private static bool IsTokenValid(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return false;
        
        try
        {
            var jwtSecurityToken = new JwtSecurityToken(token);
            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }
        catch (Exception)
        {
            return false;
        }
    }
}