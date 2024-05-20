using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MecuryProduct.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<CarModel> Cars { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<ImageModel> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CarModel>()
                .HasOne(c => c.customer)
                .WithMany(m => m.cars)
                .HasForeignKey(m => m.cid)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<CarModel>()
                .HasOne(c => c.driver)
                .WithMany(m => m.driver_cars)
                .HasForeignKey(m => m.driver_id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<CarModel>()
                .HasOne(c => c.created_by)
                .WithMany(m => m.cars)
                .HasForeignKey(m => m.created_by_id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<NoteModel>()
                .HasOne(c => c.vehicle)
                .WithMany(m => m.notes)
                .HasForeignKey(m => m.veh_id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<NoteModel>()
                .HasOne(c => c.created_by)
                .WithMany(m => m.notes)
                .HasForeignKey(m => m.created_by_id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<CustomerModel>()
                .HasOne(c => c.created_by)
                .WithMany(m => m.customers)
                .HasForeignKey(m => m.created_by_id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ImageModel>()
                .HasOne(c => c.car)
                .WithMany(m => m.images)
                .HasForeignKey(m => m.veh_id)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
