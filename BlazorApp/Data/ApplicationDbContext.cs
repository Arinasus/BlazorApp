using BlazorApp.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<TherapistProfile> TherapistProfiles { get; set; }
        public DbSet<Child> Children { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TherapistProfile>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<TherapistProfile>(p => p.UserId);
        }
    }
}
