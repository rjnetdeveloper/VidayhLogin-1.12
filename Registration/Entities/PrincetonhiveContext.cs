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

        public virtual DbSet<TblCityMaster> TblCityMaster { get; set; }
        public virtual DbSet<TblClassMaster> TblClassMaster { get; set; }
        public virtual DbSet<TblCountryMaster> TblCountryMaster { get; set; }
        public virtual DbSet<TblEnquiry> TblEnquiry { get; set; }
        public virtual DbSet<TblExceptionLog> TblExceptionLog { get; set; }
        public virtual DbSet<TblRegistration> TblRegistration { get; set; }
        public virtual DbSet<TblRoleMaster> TblRoleMaster { get; set; }
        public virtual DbSet<TblSchool> TblSchool { get; set; }
        public virtual DbSet<TblSectionMaster> TblSectionMaster { get; set; }
        public virtual DbSet<TblSessionMaster> TblSessionMaster { get; set; }
        public virtual DbSet<TblStateMaster> TblStateMaster { get; set; }
        public virtual DbSet<TblStudentRegistration> TblStudentRegistration { get; set; }
        public virtual DbSet<TblUserAuth> TblUserAuth { get; set; }
        public virtual DbSet<TblUserLoginHst> TblUserLoginHst { get; set; }
        public virtual DbSet<Tblclassmap> Tblclassmap { get; set; }
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
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TblCityMaster>(entity =>
            {
                entity.HasKey(e => e.Cityid)
                    .HasName("tblCityMaster_pkey");

                entity.ToTable("tblCityMaster");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<TblClassMaster>(entity =>
            {
                entity.HasKey(e => e.Classid)
                    .HasName("tblClassMaster_pkey");

                entity.ToTable("tblClassMaster");

                entity.Property(e => e.ClassIdBelouga).HasMaxLength(100);

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(5000);

                entity.Property(e => e.SchoolId).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(1000);

                entity.Property(e => e.TeacherId).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
            });

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

            modelBuilder.Entity<TblEnquiry>(entity =>
            {
                entity.HasKey(e => e.EnquiryId)
                    .HasName("tblEnquiry_pkey");

                entity.ToTable("tblEnquiry");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50000);

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.MobileNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SchoolName)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<TblExceptionLog>(entity =>
            {
                entity.ToTable("tblException_Log");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ErrorCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ErrorDescription)
                    .IsRequired()
                    .HasMaxLength(50000);

                entity.Property(e => e.LogDateTime).HasColumnType("date");
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

                entity.Property(e => e.FirstName).HasMaxLength(500);

                entity.Property(e => e.Gender).HasMaxLength(500);

                entity.Property(e => e.LastName).HasMaxLength(500);

                entity.Property(e => e.Mobileno).HasMaxLength(100);

                entity.Property(e => e.Photo).HasMaxLength(500);

                entity.Property(e => e.PostalCode).HasMaxLength(100);

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

                entity.Property(e => e.VendorStatus).HasColumnType("character varying");
            });

            modelBuilder.Entity<TblRoleMaster>(entity =>
            {
                entity.HasKey(e => e.Roleid)
                    .HasName("tblRoleMaster_pkey");

                entity.ToTable("tblRoleMaster");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RoleType)
                    .IsRequired()
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<TblSchool>(entity =>
            {
                entity.HasKey(e => e.Schoolid)
                    .HasName("tblSchool_pkey");

                entity.ToTable("tblSchool");

                entity.Property(e => e.ContactEmail).HasMaxLength(50);

                entity.Property(e => e.ContactPersonMobile).HasMaxLength(50);

                entity.Property(e => e.ContactPersonName).HasMaxLength(100);

                entity.Property(e => e.SchoolAddress).HasMaxLength(5000);

                entity.Property(e => e.SchoolCode).HasMaxLength(500);

                entity.Property(e => e.SchoolDescription).HasMaxLength(1000);

                entity.Property(e => e.SchoolEmail).HasMaxLength(50);

                entity.Property(e => e.SchoolIdBelouga).HasMaxLength(100);

                entity.Property(e => e.SchoolLogo).HasMaxLength(100);

                entity.Property(e => e.SchoolName).HasMaxLength(1000);

                entity.Property(e => e.SchoolPhone).HasMaxLength(50);

                entity.Property(e => e.SchoolPostalCode).HasMaxLength(50);

                entity.Property(e => e.SchoolWebsite).HasMaxLength(50);
            });

            modelBuilder.Entity<TblSectionMaster>(entity =>
            {
                entity.HasKey(e => e.Sectionid)
                    .HasName("tblSectionMaster_pkey");

                entity.ToTable("tblSectionMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblSessionMaster>(entity =>
            {
                entity.HasKey(e => e.Sessionid)
                    .HasName("tblSessionMaster_pkey");

                entity.ToTable("tblSessionMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.SessionName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
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

                entity.Property(e => e.ClassName).HasColumnType("character varying");

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

                entity.Property(e => e.SchoolName).HasMaxLength(100);

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

            modelBuilder.Entity<TblUserAuth>(entity =>
            {
                entity.HasKey(e => e.UserAuthId)
                    .HasName("tblUserAuth_pkey");

                entity.ToTable("tblUserAuth");

                entity.Property(e => e.ModifyDate).HasColumnType("date");

                entity.Property(e => e.UserAuthorities)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UserStatus)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TblUserLoginHst>(entity =>
            {
                entity.HasKey(e => e.UserLoginHstid)
                    .HasName("tblUserLogin_HST_pkey");

                entity.ToTable("tblUserLogin_HST");

                entity.Property(e => e.UserLoginHstid).HasColumnName("UserLoginHSTId");

                entity.Property(e => e.LoginDateTime).HasColumnType("date");

                entity.Property(e => e.LogoutDateTime).HasColumnType("date");
            });

            modelBuilder.Entity<Tblclassmap>(entity =>
            {
                entity.HasKey(e => e.ClassmapId)
                    .HasName("tblclassmap_pkey");

                entity.ToTable("tblclassmap");

                entity.Property(e => e.ClassIdBelouga).HasColumnType("character varying");

                entity.Property(e => e.ClassName).HasColumnType("character varying");

                entity.Property(e => e.SchoolName).HasColumnType("character varying");

                entity.Property(e => e.TeacheId)
                    .HasColumnName("TeacheID")
                    .HasColumnType("character varying");
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
