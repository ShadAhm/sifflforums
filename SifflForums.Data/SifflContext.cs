using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SifflForums.Data.Entities;
using SifflForums.Data.Interfaces;
using SifflForums.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SifflForums.Data
{
    public class SifflContext : IdentityDbContext<ApplicationUser>
    {
        public IUserResolverService _userResolverService { get; }

        public SifflContext(DbContextOptions options, IUserResolverService userResolverService) : base(options)
        {
            _userResolverService = userResolverService;
        }

        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Upvote> Upvotes { get; set; }
        public DbSet<VotingBox> VotingBoxes { get; set; }
        public DbSet<ForumSection> ForumSections { get; set; }
        public DbSet<BlacklistedPassword> BlacklistedPasswords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetConstraints();
            modelBuilder.GenerateStringKeys();
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

        // All timestamps are stored as UTC, but SQL Server returns them with Kind=Unspecified,
        // which serializes without a "Z" and makes browsers read them as local time.
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();
        }

        private class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
        {
            public UtcDateTimeConverter()
                : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            {
            }
        }

        public override int SaveChanges()
        {
            ApplyAbpConcepts();
            var result = base.SaveChanges();
            return result;
        }

        protected virtual void ApplyAbpConcepts()
        {
            var userId = GetAuditUserId();

            var entries = ChangeTracker.Entries().ToList();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        CheckAndSetId(entry);
                        SetCreationAuditProperties(entry, userId);
                        break;
                    case EntityState.Modified:
                        SetModificationAuditProperties(entry, userId);
                        break;
                }
            }
        }

        private void SetModificationAuditProperties(EntityEntry entry, string userId)
        {
            var entity = entry.Entity as IAuditableEntity;

            if(entity != null)
            {
                entity.ModifiedAtUtc = DateTime.UtcNow;
                entity.ModifiedBy = userId;
            }
        }

        private void SetCreationAuditProperties(EntityEntry entry, string userId)
        {
            var entity = entry.Entity as IAuditableEntity;

            if (entity != null)
            {
                entity.CreatedAtUtc = DateTime.UtcNow;
                entity.CreatedBy = userId;
                entity.ModifiedAtUtc = DateTime.UtcNow;
                entity.ModifiedBy = userId;
            }
        }

        private string GetAuditUserId()
        {
            return _userResolverService.GetUserId();
        }

        protected virtual void CheckAndSetId(EntityEntry entry)
        {
            var entity = entry.Entity as IEntity<string>;
            if (entity != null && string.IsNullOrWhiteSpace(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }
        }
    }
}
