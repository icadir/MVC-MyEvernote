using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<EvernoteUser> EvernoteUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //fluentApı ilişkili tabloları silme olayı 
            //modelBuilder.Entity<Note>()
            //      .HasMany(x => x.Comments)
            //      .WithRequired(c => c.Note)
            //      .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Note>()
            //      .HasMany(x => x.Likes)
            //      .WithRequired(c => c.Note)
            //      .WillCascadeOnDelete(true);



        }
    }
}
