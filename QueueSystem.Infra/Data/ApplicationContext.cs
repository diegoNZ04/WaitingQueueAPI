using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Entities;

namespace QueueSystem.Infra.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;

    public DbSet<Queue> Queues { get; set; } = null!;

    public DbSet<Background> Backgrounds { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasOne(c => c.Queue)
            .WithMany(q => q.Clients)
            .HasForeignKey(c => c.QueueId);

        modelBuilder.Entity<Background>()
            .HasOne(b => b.Client)
            .WithMany()
            .HasForeignKey(b => b.ClientId);
    }
}