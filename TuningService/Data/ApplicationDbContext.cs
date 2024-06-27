using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TuningService.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<File> Files { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<TuningDetail> TuningDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>()
            .HasMany(c => c.TuningDetails)
            .WithOne(td => td.Car)
            .HasForeignKey(td => td.CarId);

            base.OnModelCreating(builder);
        }
    }
}
