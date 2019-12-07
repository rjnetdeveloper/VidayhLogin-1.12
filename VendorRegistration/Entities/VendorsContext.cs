using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VendorRegistration.Entities
{
    public partial class VendorsContext : DbContext
    {
        public VendorsContext()
        {
        }

        public VendorsContext(DbContextOptions<VendorsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VendorApi> VendorApi { get; set; }
        public virtual DbSet<VendorsRegistration> VendorsRegistration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=103.118.157.29;Port=5432;Database=Vendors;User Id=postgres;Password=princetonhive@123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendorApi>(entity =>
            {
                entity.ToTable("VendorAPI");

                entity.Property(e => e.Type).HasColumnType("character varying");

                entity.Property(e => e.VendorApi1)
                    .HasColumnName("VendorAPI")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<VendorsRegistration>(entity =>
            {
                entity.HasKey(e => e.VendorId)
                    .HasName("VendorsRegistration_pkey");

                entity.Property(e => e.Address).HasColumnType("character varying");

                entity.Property(e => e.City).HasColumnType("character varying");

                entity.Property(e => e.ContactPerson).HasColumnType("character varying");

                entity.Property(e => e.Country).HasColumnType("character varying");

                entity.Property(e => e.Email).HasColumnType("character varying");

                entity.Property(e => e.MobileNo).HasColumnType("character varying");

                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.PhoneNo).HasColumnType("character varying");

                entity.Property(e => e.Pincode).HasColumnType("character varying");

                entity.Property(e => e.State).HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
