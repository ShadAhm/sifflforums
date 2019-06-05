using Microsoft.EntityFrameworkCore;
using ShadAhm.SifflForums.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadAhm.SifflForums.Data
{
    public class SifflContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=C:\Sample_db\Sample.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(new Comment { CommentId = 1, Text = "http://sample.com" });
        }
    }
}
