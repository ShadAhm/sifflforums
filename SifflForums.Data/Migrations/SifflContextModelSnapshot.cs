﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SifflForums.Data;

namespace SifflForums.Data.Migrations
{
    [DbContext(typeof(SifflContext))]
    partial class SifflContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("SifflForums.Data.Entities.BlacklistedPassword", b =>
                {
                    b.Property<int>("BlacklistedPasswordId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.HasKey("BlacklistedPasswordId");

                    b.ToTable("BlacklistedPasswords");
                });

            modelBuilder.Entity("SifflForums.Data.Entities.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAtUtc");

                    b.Property<int>("ModifiedBy");

                    b.Property<int>("SubmissionId");

                    b.Property<string>("Text");

                    b.Property<int>("UserId");

                    b.HasKey("CommentId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("SubmissionId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SifflForums.Data.Entities.Submission", b =>
                {
                    b.Property<int>("SubmissionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAtUtc");

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("SubmissionId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("UserId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("SifflForums.Data.Entities.Upvote", b =>
                {
                    b.Property<int>("UpvoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CommentId");

                    b.Property<int?>("SubmissionId");

                    b.Property<int>("UserId");

                    b.Property<int>("Weight");

                    b.HasKey("UpvoteId");

                    b.HasIndex("CommentId");

                    b.HasIndex("SubmissionId");

                    b.HasIndex("UserId");

                    b.ToTable("Upvotes");
                });

            modelBuilder.Entity("SifflForums.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<DateTime>("LastPasswordResetUtc");

                    b.Property<string>("Password");

                    b.Property<DateTime>("RegisteredAtUtc");

                    b.Property<string>("Salt");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SifflForums.Data.Entities.Comment", b =>
                {
                    b.HasOne("SifflForums.Data.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SifflForums.Data.Entities.User", "Modifier")
                        .WithMany()
                        .HasForeignKey("ModifiedBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SifflForums.Data.Entities.Submission", "Submission")
                        .WithMany("Comments")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SifflForums.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SifflForums.Data.Entities.Submission", b =>
                {
                    b.HasOne("SifflForums.Data.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SifflForums.Data.Entities.User", "Modifier")
                        .WithMany()
                        .HasForeignKey("ModifiedBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SifflForums.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SifflForums.Data.Entities.Upvote", b =>
                {
                    b.HasOne("SifflForums.Data.Entities.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("SifflForums.Data.Entities.Submission", "Submission")
                        .WithMany()
                        .HasForeignKey("SubmissionId");

                    b.HasOne("SifflForums.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
