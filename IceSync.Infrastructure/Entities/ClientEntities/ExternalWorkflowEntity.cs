namespace IceSync.Infrastructure.Entities.ClientEntities;

using System.Text.Json.Serialization;

internal record ExternalWorkflowEntity
{
    [JsonPropertyName("id")]
    public int Id { get; init; }
    
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("isActive")]
    public bool IsActive { get; init; }
    
    public bool IsRunning { get; init; }
    
    [JsonPropertyName("multiExecBehavior")]
    public string MultiExecBehaviour { get; init; }
}