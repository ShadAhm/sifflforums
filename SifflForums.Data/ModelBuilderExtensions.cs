using Microsoft.EntityFrameworkCore;
using SifflForums.Data.Entities;
using System;

namespace SifflForums.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SetConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasIndex(a => a.VotingBoxId).IsUnique();
            modelBuilder.Entity<Submission>().HasIndex(a => a.VotingBoxId).IsUnique();
            modelBuilder.Entity<Upvote>().HasAlternateKey(a => new { a.VotingBoxId, a.UserId });
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ForumSection>().HasData(new ForumSection { ForumSectionId = 1, Name = "General", Description = "General Discussions", CreatedAtUtc = DateTime.UtcNow, CreatedBy = null, ModifiedAtUtc = DateTime.UtcNow, ModifiedBy = null });
            //modelBuilder.Entity<VotingBox>().HasData(new VotingBox { VotingBoxId = 1 });
            //modelBuilder.Entity<Submission>().HasData(new Submission { SubmissionId = 1, VotingBoxId = 1, UserId = 1, ForumSectionId = 1, Title = "Welcome to Siffl Forums", Text = "Simple Forums for Learning", CreatedAtUtc = DateTime.UtcNow, CreatedBy = null, ModifiedAtUtc = DateTime.UtcNow, ModifiedBy = 1 });
        }
    }
}
