namespace IceSync.BL.Configurations;

public record AuthorizationSettings
{
    public string ApiVersion { get; set; }
    public string ApiCompanyId { get; set; }
    public string ApiUserId { get; set; }
    public string ApiUserSecret { get; set; }
}