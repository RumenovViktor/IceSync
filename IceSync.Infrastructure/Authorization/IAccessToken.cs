namespace IceSync.Infrastructure.Authorization;

public interface IAccessToken
{
    string Token { get; }

    void SetToken(string token);
}