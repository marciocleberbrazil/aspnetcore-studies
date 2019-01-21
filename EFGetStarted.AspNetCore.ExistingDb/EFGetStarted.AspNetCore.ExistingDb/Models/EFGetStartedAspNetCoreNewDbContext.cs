using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
{
    public partial class EFGetStartedAspNetCoreNewDbContext : DbContext
    {
        public EFGetStartedAspNetCoreNewDbContext()
        {
        }

        public EFGetStartedAspNetCoreNewDbContext(DbContextOptions<EFGetStartedAspNetCoreNewDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blogs>(entity =>
            {
                entity.HasKey(e => e.BlogId);
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.PostId);

                entity.HasIndex(e => e.BlogId);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BlogId);
            });
        }
    }
}
