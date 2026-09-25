using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SifflForums.Data.Entities;
using System;
using System.Linq;

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

        // EF refuses to track an entity with a null key, so string Ids must be generated on Add(),
        // not in SaveChanges. Seeded rows keep their explicit Ids.
        public static void GenerateStringKeys(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(EntityBase).IsAssignableFrom(t.ClrType))
                .ToList();

            foreach (var entityType in entityTypes)
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(EntityBase.Id))
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<StringValueGenerator>();
            }
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed values must be deterministic, otherwise EF sees a model change on every build
            var seededAtUtc = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<ForumSection>().HasData(new ForumSection { Id = "8f1c6a3e-2b1d-4c1e-9f0a-5d3b7e2a1c40", Name = "General", Description = "General Discussions", CreatedAtUtc = seededAtUtc, CreatedBy = null, ModifiedAtUtc = seededAtUtc, ModifiedBy = null });
            //modelBuilder.Entity<VotingBox>().HasData(new VotingBox { VotingBoxId = 1 });
            //modelBuilder.Entity<Submission>().HasData(new Submission { SubmissionId = 1, VotingBoxId = 1, UserId = 1, ForumSectionId = 1, Title = "Welcome to Siffl Forums", Text = "Simple Forums for Learning", CreatedAtUtc = DateTime.UtcNow, CreatedBy = null, ModifiedAtUtc = DateTime.UtcNow, ModifiedBy = 1 });
        }
    }
}
