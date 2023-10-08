using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace alpha3.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<CoachProfile> CoachProfiles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<MemberSchedule> MemberSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MemberSchedule Composite Key
            modelBuilder.Entity<MemberSchedule>()
                .HasKey(ms => new { ms.MemberId, ms.ScheduleId });

            // MemberSchedule to Member relationship
            modelBuilder.Entity<MemberSchedule>()
                .HasOne(ms => ms.Member)
                .WithMany(m => m.MemberSchedules)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading deletes

            // MemberSchedule to Schedule relationship
            modelBuilder.Entity<MemberSchedule>()
                .HasOne(ms => ms.Schedule)
                .WithMany(s => s.MemberSchedules)
                .HasForeignKey(ms => ms.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading deletes

            // ApplicationUser to CoachProfile relationship
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.CoachProfile)
                .WithOne(b => b.User)
                .HasForeignKey<CoachProfile>(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading deletes

            // Relationship between Schedule and Coach (ApplicationUser)
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Coach)
                .WithMany(u => u.Schedules)
                .OnDelete(DeleteBehavior.Restrict);  // You might want to adjust this based on your needs
           
    

        }
    }
}
