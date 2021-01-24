﻿using Microsoft.EntityFrameworkCore;
using MyPhoneBook.DataLayer.Entity;
using System;
using System.Linq;

namespace MyPhoneBook.DataLayer
{
    public class MyPhoneBookContext : DbContext
    {
        public MyPhoneBookContext(DbContextOptions<MyPhoneBookContext> options) : base(options)
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