using Microsoft.EntityFrameworkCore;
using UnternährerDominicLB_295.Model;

namespace UnternährerDominicLB_295.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Owner2Car> Owners2Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner2Car>()
                .HasKey(ba => new { ba.CarId, ba.OwnerId });

            base.OnModelCreating(modelBuilder);
        }

    }
}
