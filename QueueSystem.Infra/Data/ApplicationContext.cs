using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<ClientModel> Clients { get; set; } = null!;

    public DbSet<QueueModel> Queues { get; set; } = null!;

    public DbSet<BackgroundModel> Backgrounds { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientModel>()
            .HasOne(c => c.Queue)
            .WithMany(q => q.Clients)
            .HasForeignKey(c => c.QueueId);

        modelBuilder.Entity<BackgroundModel>()
            .HasOne(b => b.Client)
            .WithMany()
            .HasForeignKey(b => b.ClientId);
    }
}