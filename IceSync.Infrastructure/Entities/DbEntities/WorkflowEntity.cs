namespace IceSync.Infrastructure.Entities.DbEntities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class WorkflowEntity
{
    [Key]  
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }
    
    public int WorkflowId { get; set; }

    public string WorkflowName { get; set; }
    public bool IsActive { get; set; }
    public bool IsRunning { get; set; }
    public string MultiExecBehaviour { get; set; }
}