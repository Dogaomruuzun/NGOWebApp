﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NGOAppMVC.DBModels
{
    public partial class DCodeNGOdataNGOsqliteContext : DbContext
    {
        public DCodeNGOdataNGOsqliteContext()
        {
        }

        public DCodeNGOdataNGOsqliteContext(DbContextOptions<DCodeNGOdataNGOsqliteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Donation> Donation { get; set; }
        public virtual DbSet<Indigent> Indigent { get; set; }
        public virtual DbSet<IndigentDependents> IndigentDependents { get; set; }
        public virtual DbSet<LkbDependentRelation> LkbDependentRelation { get; set; }
        public virtual DbSet<LkpApprovementStatus> LkpApprovementStatus { get; set; }
        public virtual DbSet<LkpDonableItem> LkpDonableItem { get; set; }
        public virtual DbSet<LkpEducationalStatus> LkpEducationalStatus { get; set; }
        public virtual DbSet<LkpEmploymentStatus> LkpEmploymentStatus { get; set; }
        public virtual DbSet<LkpProfession> LkpProfession { get; set; }
        public virtual DbSet<LkpRegions> LkpRegions { get; set; }
        public virtual DbSet<LkpRole> LkpRole { get; set; }
        public virtual DbSet<Ngouser> Ngouser { get; set; }
        public virtual DbSet<NgouserRoles> NgouserRoles { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }
        public virtual DbSet<WarehouseAssets> WarehouseAssets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donation>(entity =>
            {
                entity.Property(e => e.DonationDate).IsRequired();

                entity.Property(e => e.NgouserId).HasColumnName("NGOUserId");

                entity.HasOne(d => d.Ngouser)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.NgouserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.RegionId);
            });

            modelBuilder.Entity<Indigent>(entity =>
            {
                entity.HasKey(e => e.NgouserId);

                entity.Property(e => e.NgouserId)
                    .ValueGeneratedNever()
                    .HasColumnName("NGOUserId");

                entity.HasOne(d => d.DonableItem)
                    .WithMany(p => p.Indigent)
                    .HasForeignKey(d => d.DonableItemId);

                entity.HasOne(d => d.Ngouser)
                    .WithOne(p => p.Indigent)
                    .HasForeignKey<Indigent>(d => d.NgouserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Indigent)
                    .HasForeignKey(d => d.RegionId);
            });

            modelBuilder.Entity<IndigentDependents>(entity =>
            {
                entity.Property(e => e.NgouserId).HasColumnName("NGOUserId");

                entity.HasOne(d => d.DependentRelation)
                    .WithMany(p => p.IndigentDependents)
                    .HasForeignKey(d => d.DependentRelationId);

                entity.HasOne(d => d.EducationStatus)
                    .WithMany(p => p.IndigentDependents)
                    .HasForeignKey(d => d.EducationStatusId);

                entity.HasOne(d => d.EmploymentStatus)
                    .WithMany(p => p.IndigentDependents)
                    .HasForeignKey(d => d.EmploymentStatusId);

                entity.HasOne(d => d.Ngouser)
                    .WithMany(p => p.IndigentDependents)
                    .HasForeignKey(d => d.NgouserId);
            });

            modelBuilder.Entity<LkbDependentRelation>(entity =>
            {
                entity.ToTable("lkb_DependentRelation");
            });

            modelBuilder.Entity<LkpApprovementStatus>(entity =>
            {
                entity.ToTable("lkp_ApprovementStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<LkpDonableItem>(entity =>
            {
                entity.ToTable("lkp_DonableItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DonableAmountType).IsRequired();

                entity.Property(e => e.DonableTypeName).IsRequired();
            });

            modelBuilder.Entity<LkpEducationalStatus>(entity =>
            {
                entity.ToTable("lkp_EducationalStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<LkpEmploymentStatus>(entity =>
            {
                entity.ToTable("lkp_EmploymentStatus");
            });

            modelBuilder.Entity<LkpProfession>(entity =>
            {
                entity.ToTable("lkp_Profession");
            });

            modelBuilder.Entity<LkpRegions>(entity =>
            {
                entity.ToTable("lkp_Regions");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.GeoLocationUrl).HasColumnName("GeoLocationURL");
            });

            modelBuilder.Entity<LkpRole>(entity =>
            {
                entity.ToTable("lkp_Role");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RoleName).IsRequired();
            });

            modelBuilder.Entity<Ngouser>(entity =>
            {
                entity.ToTable("NGOUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.HasOne(d => d.ApprovementStatus)
                    .WithMany(p => p.Ngouser)
                    .HasForeignKey(d => d.ApprovementStatusId);
            });

            modelBuilder.Entity<NgouserRoles>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NGOUserRoles");

                entity.Property(e => e.NgouserId).HasColumnName("NGOUserId");

                entity.HasOne(d => d.Ngouser)
                    .WithMany()
                    .HasForeignKey(d => d.NgouserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.HasKey(e => e.NgouserId);

                entity.Property(e => e.NgouserId)
                    .ValueGeneratedNever()
                    .HasColumnName("NGOUserId");

                entity.HasOne(d => d.Ngouser)
                    .WithOne(p => p.Volunteer)
                    .HasForeignKey<Volunteer>(d => d.NgouserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.Volunteer)
                    .HasForeignKey(d => d.ProfessionId);
            });

            modelBuilder.Entity<WarehouseAssets>(entity =>
            {
                entity.HasKey(z=>z.Id);

                entity.HasOne(d => d.Donation)
                    .WithMany()
                    .HasForeignKey(d => d.DonationId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}