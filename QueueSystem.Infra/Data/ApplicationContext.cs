using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<ClientModel> Clients { get; set; } = null!;

        public DbSet<QueueModel> Queues { get; set; } = null!;

        public DbSet<BackgroundModel> Backgrounds { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientModel>()
                .HasOne<BackgroundModel>()
                .WithOne()
                .HasForeignKey<BackgroundModel>(e => e.ClientId)
                .IsRequired();

            modelBuilder.Entity<QueueModel>()
                .HasMany(e => e.Clients)
                .WithOne()
                .HasForeignKey("QueueId")
                .IsRequired();
        }
    }
}