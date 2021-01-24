using Microsoft.EntityFrameworkCore;
using MyLittleBlackBook.DataLayer.Entity;
using System;
using System.Linq;

namespace MyLittleBlackBook.DataLayer
{
    public class MyLittleBlackBookContext : DbContext
    {
        public MyLittleBlackBookContext(DbContextOptions<MyLittleBlackBookContext> options) : base(options)
        {

        }

        public DbSet<PhoneBook> PhoneBooks { get; set; }
        public DbSet<Entry> Entries { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<BaseEntity>()
        //            .Property(s => s.Id)
        //            .ValueGeneratedOnAdd();
        //}

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
