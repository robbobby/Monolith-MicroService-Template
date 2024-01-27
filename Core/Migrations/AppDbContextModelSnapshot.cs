﻿// <auto-generated />
using System;
using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entity.Identity.OrganisationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Organisations");
                });

            modelBuilder.Entity("Core.Entity.Identity.TokenEntity", b =>
                {
                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("ExpiresAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("AccessToken", "RefreshToken");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Core.Entity.Identity.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Entity.Identity.UserOrganisationEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "OrganisationId");

                    b.HasIndex("OrganisationId");

                    b.ToTable("UserOrganisation");
                });

            modelBuilder.Entity("Core.Entity.Project.ProjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Core.Entity.Project.TicketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AssignedToId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("uuid");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("OrganisationId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Core.Entity.Identity.TokenEntity", b =>
                {
                    b.HasOne("Core.Entity.Identity.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entity.Identity.UserOrganisationEntity", b =>
                {
                    b.HasOne("Core.Entity.Identity.OrganisationEntity", "Organisation")
                        .WithMany("Users")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entity.Identity.UserEntity", "User")
                        .WithMany("Organisations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entity.Project.ProjectEntity", b =>
                {
                    b.HasOne("Core.Entity.Identity.OrganisationEntity", "Organisation")
                        .WithMany("Projects")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisation");
                });

            modelBuilder.Entity("Core.Entity.Project.TicketEntity", b =>
                {
                    b.HasOne("Core.Entity.Identity.UserEntity", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AssignedToId");

                    b.HasOne("Core.Entity.Identity.OrganisationEntity", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entity.Project.ProjectEntity", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.Navigation("AssignedTo");

                    b.Navigation("Organisation");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Core.Entity.Identity.OrganisationEntity", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Core.Entity.Identity.UserEntity", b =>
                {
                    b.Navigation("Organisations");
                });
#pragma warning restore 612, 618
        }
    }
}
