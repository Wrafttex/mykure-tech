
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppLockerService.Models
{
  public class AppLockerServiceContext : DbContext
  {
    public DbSet<App> Apps { get; set; }

    public AppLockerServiceContext(DbContextOptions<AppLockerServiceContext> options) 
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);        
        MapApps(modelBuilder.Entity<App>());
    }

    // Since table names are lowercase but C# properties use PascalCasing, we
    // need to map every property of the App class to its corresponding column name
    // in the apps table.
    private void MapApps(EntityTypeBuilder<App> entityBuilder)
    {
        entityBuilder.HasKey(x => x.Id);
        entityBuilder.ToTable("app");

        entityBuilder.Property(x => x.Id).HasColumnName("id");
        entityBuilder.Property(x => x.Code).HasColumnName("code");
        entityBuilder.Property(x => x.Name).HasColumnName("name");
        entityBuilder.Property(x => x.Description).HasColumnName("description");
        entityBuilder.Property(x => x.IsLocked).HasColumnName("is_locked");
        entityBuilder.Property(x => x.Reason).HasColumnName("reason");
    }
  }
}
