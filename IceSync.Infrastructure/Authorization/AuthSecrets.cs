namespace IceSync.Infrastructure.Authorization;

public record AuthSecrets
{
    public string ApiCompanyId { get; set; }
    public string ApiUserId { get; set; }
    public string ApiUserSecret { get; set; }
}