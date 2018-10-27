using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BlogPostAPI.Models.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
         .HasRequired<User>(s => s.User)
         .WithMany(g => g.Posts)
         .HasForeignKey<int>(s => s.Id);

            /*
           modelBuilder.Entity<Comment>()
           .HasRequired<User>(s => s.User)
           .WithMany(g => g.Comments)
           .HasForeignKey<int>(s => s.UserID);
           
           modelBuilder.Entity<Comment>()
          .HasRequired<Post>(s => s.Post)
          .WithMany(g => g.Comments)
          .HasForeignKey<int>(s => s.PostID);
          */

            modelBuilder.Entity<Post>()
          .HasRequired<Category>(s => s.Category)
          .WithMany(g => g.Posts)
          .HasForeignKey<int>(s => s.CategoryId);
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // Database.SetInitializer<DatabaseContext>(null);
            // base.OnModelCreating(modelBuilder);
        }
    }
}