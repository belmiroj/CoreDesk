using CoreDesk.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreDesk.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketInteraction> TicketInteractions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketInteraction>()
                .HasOne(ti => ti.Ticket)
                .WithMany(t => t.Interactions)
                .HasForeignKey(ti => ti.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}