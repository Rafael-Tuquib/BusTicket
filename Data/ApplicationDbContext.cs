using Microsoft.EntityFrameworkCore;
using BusTicketing.Models;

namespace BusTicketing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Barangay> Barangays { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Barangay>()
                .HasOne(b => b.Municipality)
                .WithMany(m => m.Barangays)
                .HasForeignKey(b => b.MunicipalityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.FromBarangay)
                .WithMany()
                .HasForeignKey(t => t.FromBarangayId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.ToBarangay)
                .WithMany()
                .HasForeignKey(t => t.ToBarangayId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}