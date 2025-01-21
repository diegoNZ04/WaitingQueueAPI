using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Entities;

namespace QueueSystem.Infra.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = null!;

        public DbSet<Queue> Queues { get; set; } = null!;

        public DbSet<Background> Backgrounds { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne<Background>()
                .WithOne()
                .HasForeignKey<Background>(e => e.ClientId)
                .IsRequired();

            modelBuilder.Entity<Queue>()
                .HasMany(e => e.Clients)
                .WithOne()
                .HasForeignKey("QueueId")
                .IsRequired();
        }
    }
}