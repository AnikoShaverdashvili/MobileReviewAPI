using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.Data
{
    public class MobileReviewDbContext : DbContext
    {
        public MobileReviewDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<MobileCategory> MobileCategories { get; set; }
        public DbSet<MobileOwner> MobileOwners { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MobileCategory>()
                .HasKey(mc => new { mc.MobileId, mc.CategoryId });
            modelBuilder.Entity<MobileCategory>()
                .HasOne(m => m.Mobile)
                .WithMany(mc => mc.MobileCategories)
                .HasForeignKey(m => m.MobileId);
            modelBuilder.Entity<MobileCategory>()
                .HasOne(m => m.Category)
                .WithMany(mc => mc.MobileCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<MobileOwner>()
                .HasKey(mo => new { mo.MobileId, mo.OwnerId });
            modelBuilder.Entity<MobileOwner>()
                .HasOne(m => m.Mobile)
                .WithMany(mo => mo.MobileOwners)
                .HasForeignKey(c => c.MobileId);
            modelBuilder.Entity<MobileOwner>()
                .HasOne(m => m.Owner)
                .WithMany(mo => mo.MobileOwners)
                .HasForeignKey(c => c.OwnerId);
        }
    }
}