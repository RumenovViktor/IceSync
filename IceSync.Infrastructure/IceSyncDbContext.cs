namespace IceSync.Infrastructure;

using Entities.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class IceSyncDbContext : DbContext
{
    protected readonly IConfiguration Configuration;
    
    public IceSyncDbContext()
    {
    }

    public IceSyncDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Port=5432;Database=IceSync;User Id=postgres;Password=Kokali123@;");
    }
    
    public DbSet<WorkflowEntity> Workflows { get; set; } = default!;
}