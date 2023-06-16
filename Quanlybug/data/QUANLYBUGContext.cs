using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Quanlybug.data
{
    public partial class QUANLYBUGContext : DbContext
    {
        public QUANLYBUGContext()
        {
        }

        public QUANLYBUGContext(DbContextOptions<QUANLYBUGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Party> Parties { get; set; } = null!;
        public virtual DbSet<PartyNumber> PartyNumbers { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<UserMember> UserMembers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-O10K6DV\\SQLEXPRESS;Database=QUANLYBUG;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Party>(entity =>
            {
                entity.HasKey(e => e.IdParty)
                    .HasName("PK__PARTY__DC19A3710BD6A474");

                entity.ToTable("PARTY");

                entity.Property(e => e.IdParty).HasColumnName("ID_PARTY");

                entity.Property(e => e.NameParty)
                    .HasMaxLength(100)
                    .HasColumnName("NAME_PARTY");

                entity.Property(e => e.Number).HasColumnName("NUMBER");
            });

            modelBuilder.Entity<PartyNumber>(entity =>
            {
                entity.HasKey(e => e.IdPartyNumber)
                    .HasName("PK__PARTY_NU__3871505B6CC66089");

                entity.ToTable("PARTY_NUMBER");

                entity.Property(e => e.IdPartyNumber).HasColumnName("ID_PARTY_NUMBER");

                entity.Property(e => e.IdParty).HasColumnName("ID_PARTY");

                entity.Property(e => e.NamePartyNumber)
                    .HasMaxLength(200)
                    .HasColumnName("NAME_PARTY_NUMBER");

                entity.HasOne(d => d.IdPartyNavigation)
                    .WithMany(p => p.PartyNumbers)
                    .HasForeignKey(d => d.IdParty)
                    .HasConstraintName("PARTY_NUMBER_1");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProject)
                    .HasName("PK__PROJECT__DD4373E150E59701");

                entity.ToTable("PROJECT");

                entity.Property(e => e.IdProject).HasColumnName("ID_PROJECT");

                entity.Property(e => e.ContextProject)
                    .HasMaxLength(100)
                    .HasColumnName("CONTEXT_PROJECT");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.NameProject)
                    .HasMaxLength(100)
                    .HasColumnName("NAME_PROJECT");

                entity.Property(e => e.Peformer).HasColumnName("PEFORMER");

                entity.Property(e => e.Picture)
                    .HasMaxLength(100)
                    .HasColumnName("PICTURE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("PROJECT_1");
            });

            modelBuilder.Entity<UserMember>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__USER_MEM__95F48440614719CA");

                entity.ToTable("USER_MEMBER");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .HasColumnName("ACCOUNT");

                entity.Property(e => e.NameUser)
                    .HasMaxLength(50)
                    .HasColumnName("NAME_USER");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Permission)
                    .HasMaxLength(80)
                    .HasColumnName("PERMISSION");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
