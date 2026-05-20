using BlazorApp.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<TherapistProfile> TherapistProfiles { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<TherapistReview> TherapistReviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<TherapyDiaryLog> TherapyDiaryLogs { get; set; }
        public DbSet<DiaryInvitation> DiaryInvitations { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Favorite>()
                .HasOne(f => f.TherapistProfile)
                .WithMany()
                .HasForeignKey(f => f.TherapistProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Favorite>()
                .HasOne(f => f.Lecture)
                .WithMany()
                .HasForeignKey(f => f.LectureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TherapistReview>()
                .HasOne(r => r.TherapistProfile)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.TherapistProfileId);

            builder.Entity<TherapistProfile>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<TherapistProfile>(p => p.UserId);

            builder.Entity<TherapyDiaryLog>()
                .HasOne<TherapistProfile>()
                .WithMany()
                .HasForeignKey(l => l.TherapistProfileId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
