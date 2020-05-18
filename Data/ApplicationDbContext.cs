using System;
using System.Collections.Generic;
using System.Text;
using BlogApplication.Models.Posts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Tag>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<PostTag>()
              .HasKey(x => new { x.PostId, x.TagId });

            modelBuilder.Entity<PostTag>()
           .HasKey(x => new { x.PostId, x.TagId });
            modelBuilder.Entity<PostTag>()
                .HasOne(x => x.Post)
                .WithMany(m => m.PostTags)
                .HasForeignKey(x => x.PostId);
            modelBuilder.Entity<PostTag>()
                .HasOne(x => x.Tag)
                .WithMany(e => e.Posts)
                .HasForeignKey(x => x.TagId);
        }
    }
}
