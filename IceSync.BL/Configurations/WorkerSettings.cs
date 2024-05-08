namespace IceSync.BL.Configurations;

public record WorkerSettings
{
    public TimeSpan Interval { get; init; }
}