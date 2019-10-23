﻿using Microsoft.EntityFrameworkCore;
using SifflForums.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data
{
    public class SifflContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Upvote> Upvotes { get; set; }
        public DbSet<VotingBox> VotingBoxes { get; set; }
        public DbSet<ForumSection> ForumSections { get; set; }
        public DbSet<BlacklistedPassword> BlacklistedPasswords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=C:\Sample_db\Sample.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetConstraints();
            modelBuilder.Seed(); 
        }
    }
}
