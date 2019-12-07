using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Registration.Entities
{
    public partial class PrincetonhiveContext : DbContext
    {
        public PrincetonhiveContext()
        {
        }

        public PrincetonhiveContext(DbContextOptions<PrincetonhiveContext> options)
            : base(options)
        {
        }

        
        public virtual DbSet<TblCountryMaster> TblCountryMaster { get; set; } 
        public virtual DbSet<TblRegistration> TblRegistration { get; set; }
        public virtual DbSet<TblStateMaster> TblStateMaster { get; set; }
        public virtual DbSet<TblStudentRegistration> TblStudentRegistration { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=103.118.157.29;Port=5432;Database=Princetonhive;User Id=postgres;Password=princetonhive@123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<TblCountryMaster>(entity =>
            {
                entity.HasKey(e => e.Countryid)
                    .HasName("tblCountryMaster_pkey");

                entity.ToTable("tblCountryMaster");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Phonecode)
                    .IsRequired()
                    .HasMaxLength(1000);
            });
            
            modelBuilder.Entity<TblRegistration>(entity =>
            {
                entity.HasKey(e => e.RegistrationId)
                    .HasName("tblRegistration_pkey");

                entity.ToTable("tblRegistration");

                entity.Property(e => e.Address).HasMaxLength(5000);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Class).HasMaxLength(100);

                entity.Property(e => e.ClassIdBelouga).HasMaxLength(100);

                entity.Property(e => e.DisplayName).HasMaxLength(1000);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.FatherEducation).HasMaxLength(100);

                entity.Property(e => e.FatherEmail).HasMaxLength(100);

                entity.Property(e => e.FatherMobile).HasMaxLength(100);

                entity.Property(e => e.FatherName).HasMaxLength(100);

                entity.Property(e => e.FatherOccupation).HasMaxLength(500);

                entity.Property(e => e.FirstName).HasMaxLength(500);

                entity.Property(e => e.Gender).HasMaxLength(500);

                entity.Property(e => e.LastName).HasMaxLength(500);

                entity.Property(e => e.Mobileno).HasMaxLength(100);

                entity.Property(e => e.MotherEducation).HasMaxLength(500);

                entity.Property(e => e.MotherEmail).HasMaxLength(500);

                entity.Property(e => e.MotherMobile).HasMaxLength(500);

                entity.Property(e => e.MotherName).HasMaxLength(500);

                entity.Property(e => e.MotherOccupation).HasMaxLength(500);

                entity.Property(e => e.Photo).HasMaxLength(500);

                entity.Property(e => e.PostalCode).HasMaxLength(100);

                entity.Property(e => e.SchoolCode).HasMaxLength(100);

                entity.Property(e => e.SchoolContactPerson).HasMaxLength(50);

                entity.Property(e => e.SchoolEmail).HasMaxLength(50);

                entity.Property(e => e.SchoolIdBelouga).HasMaxLength(100);

                entity.Property(e => e.SchoolLogo).HasMaxLength(100);

                entity.Property(e => e.SchoolName).HasMaxLength(100);

                entity.Property(e => e.SchoolPhone).HasMaxLength(20);

                entity.Property(e => e.Section).HasMaxLength(100);

                entity.Property(e => e.Session).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(500);

                entity.Property(e => e.TeacherIdBelouga).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(500);
            });
            
            modelBuilder.Entity<TblStateMaster>(entity =>
            {
                entity.HasKey(e => e.Stateid)
                    .HasName("tblStateMaster_pkey");

                entity.ToTable("tblStateMaster");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<TblStudentRegistration>(entity =>
            {
                entity.HasKey(e => e.StudentRegistrationId)
                    .HasName("tblStudentRegistration_pkey");

                entity.ToTable("tblStudentRegistration");

                entity.Property(e => e.Address).HasMaxLength(5000);

                entity.Property(e => e.ClassIdBelouga).HasMaxLength(100);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FatherEducation).HasMaxLength(100);

                entity.Property(e => e.FatherEmail).HasMaxLength(100);

                entity.Property(e => e.FatherMobile).HasMaxLength(100);

                entity.Property(e => e.FatherName).HasMaxLength(500);

                entity.Property(e => e.FatherOccupation).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Gender).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(500);

                entity.Property(e => e.Mobileno).HasMaxLength(100);

                entity.Property(e => e.MotherEducation).HasMaxLength(100);

                entity.Property(e => e.MotherEmail).HasMaxLength(100);

                entity.Property(e => e.MotherMobile).HasMaxLength(100);

                entity.Property(e => e.MotherName).HasMaxLength(500);

                entity.Property(e => e.MotherOccupation).HasMaxLength(100);

                entity.Property(e => e.Photo).HasMaxLength(500);

                entity.Property(e => e.PostalCode).HasMaxLength(100);

                entity.Property(e => e.SchoolCode).HasMaxLength(100);

                entity.Property(e => e.Session).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.StudentIdBelouga).HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);
            });
            
            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.Property(e => e.Captcha).HasMaxLength(500);

                entity.Property(e => e.City).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.Mobile).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UserRole).HasMaxLength(500);
            });
        }
    }
}
