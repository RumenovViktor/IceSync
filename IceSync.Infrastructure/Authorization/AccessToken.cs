namespace IceSync.Infrastructure.Authorization;

public record AccessToken : IAccessToken
{
    private readonly object Lock = new();
    
    public string Token { get; private set; }
    
    public void SetToken(string token)
    {
        lock (Lock)
        {
            Token = token;   
        }
    }
}