﻿using Microsoft.EntityFrameworkCore;
using PlayCapsViewer.Models;

namespace PlayCapsViewer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //tell the dbcontext what our tables are 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PlayCap> PlayCaps { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        //also include join tables
        public DbSet<PlayCapsCategory> PlayCapsCategories { get; set; }
        public DbSet<PlayCapsPlayer> PlayCapsPlayers { get; set; }


        //used for many-to-many relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayCapsCategory>()
                .HasKey(x => new { x.PlayCapId, x.CategoryId });

            modelBuilder.Entity<PlayCapsCategory>()
                .HasOne(x => x.PlayCap)
                .WithMany(x => x.PlayCapsCategory)
                .HasForeignKey(x => x.PlayCapId);

            modelBuilder.Entity<PlayCapsCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.PlayCapsCategory)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<PlayCapsPlayer>()
                .HasKey(x => new { x.PlayCapId, x.PlayerId });

            modelBuilder.Entity<PlayCapsPlayer>()
                .HasOne(x => x.PlayCap)
                .WithMany(x => x.PlayCapsPlayer)
                .HasForeignKey(x => x.PlayCapId);

            modelBuilder.Entity<PlayCapsPlayer>()
                .HasOne(x => x.Player)
                .WithMany(x => x.PlayCapsPlayer)
                .HasForeignKey(x => x.PlayerId);

            // Reverse navigation properties
            modelBuilder.Entity<PlayCap>()
                .HasMany(x => x.PlayCapsCategory)
                .WithOne(x => x.PlayCap)
                .HasForeignKey(x => x.PlayCapId);

            modelBuilder.Entity<Category>()
                .HasMany(x => x.PlayCapsCategory)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<PlayCap>()
                .HasMany(x => x.PlayCapsPlayer)
                .WithOne(x => x.PlayCap)
                .HasForeignKey(x => x.PlayCapId);

            modelBuilder.Entity<Player>()
                .HasMany(x => x.PlayCapsPlayer)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId);
        }

    }

}
