using Microsoft.EntityFrameworkCore;
using RealEstateAdsEntities;
using System;

namespace RealEstateAds.Data
{
    public class RealEstateAdsContext : DbContext
    {
        public RealEstateAdsContext(DbContextOptions<RealEstateAdsContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(u => u.User).WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
