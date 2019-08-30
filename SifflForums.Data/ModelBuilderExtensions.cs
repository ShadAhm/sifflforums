using Microsoft.EntityFrameworkCore;
using SifflForums.Data.Entities;

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
        }
    }
}
