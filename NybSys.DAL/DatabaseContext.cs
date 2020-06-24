using Microsoft.EntityFrameworkCore;
using NybSys.Models.DTO;

namespace NybSys.DAL
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public virtual DbSet<AccessRight> AccessRight { get; set; }
        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<Companys> Companys { get; set; }
        public virtual DbSet<Controllers> Controllers { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<DriverInfos> DriverInfos { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<EmployeeTypes> EmployeeTypes { get; set; }
        public virtual DbSet<Requisitions> Requisitions { get; set; }
        public virtual DbSet<SessionLog> SessionLog { get; set; }
        public virtual DbSet<TravelDetails> TravelDetails { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VehicleDocuments> VehicleDocuments { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }
        public virtual DbSet<VehicleTypes> VehicleTypes { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<LogWrite> LogWrite { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessRight>(entity =>
            {
                entity.Property(e => e.AccessRightName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Actions>(entity =>
            {
                entity.HasIndex(e => e.ControllerId);

                entity.Property(e => e.ActionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(30);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Controller)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.ControllerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actions_Controllers");
            });

            modelBuilder.Entity<Companys>(entity =>
            {
                entity.HasKey(e => e.CompanyId);
                entity.Property(p => p.CompanyId).ValueGeneratedOnAdd();
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            });


            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasKey(e => e.DesignationID);
                entity.Property(p => p.DesignationID).ValueGeneratedOnAdd();
                entity.Property(e => e.DesignationID).HasColumnName("DesignationID");
            });

            modelBuilder.Entity<Controllers>(entity =>
            {
                entity.Property(e => e.ControllerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(30);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
                entity.Property(p => p.DepartmentId).ValueGeneratedOnAdd();
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Departments_Companys");
            });

            modelBuilder.Entity<DriverInfos>(entity =>
            {
                entity.HasKey(e => e.DriverInfoId);

                entity.Property(p => p.DriverInfoId).ValueGeneratedOnAdd();

                entity.Property(e => e.DriverInfoId).HasColumnName("DriverInfoID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                //entity.HasOne(d => d.Employee)
                //    .WithMany(p => p.DriverInfos)
                //    .HasForeignKey(d => d.EmployeeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_DriverInfos_Employees");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DriverInfos)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                    entity.Ignore(b => b.EmployeeName);

            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(p => p.EmployeeId).ValueGeneratedOnAdd();

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.DesignationId).HasColumnName("DesignationId");


                entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");

                entity.Property(e => e.BankId).HasColumnName("BankID");

                entity.Property(e => e.BiometricId).HasColumnName("BiometricID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Dob).HasColumnName("DOB");

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");

                entity.Property(e => e.NationalId).HasColumnName("NationalID");

                entity.Property(e => e.NationalityId).HasColumnName("NationalityID");

                entity.Property(e => e.OccupassionalCategoryId).HasColumnName("OccupassionalCategoryID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Departments");

                entity.HasOne(d => d.EmployeeTypeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_EmployeeTypes");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Units");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Designation");




            });

            modelBuilder.Entity<EmployeeTypes>(entity =>
            {
                entity.HasKey(e => e.EmpTypeId);
                entity.Property(p => p.EmpTypeId).ValueGeneratedOnAdd();

                entity.Property(e => e.EmpTypeId).HasColumnName("EmpTypeID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            });

            modelBuilder.Entity<Requisitions>(entity =>
            {
                entity.HasKey(e => e.RequisitionId);
                entity.Property(p => p.RequisitionId).ValueGeneratedOnAdd();

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.Property(e => e.DriverID).HasColumnName("DriverID");


                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.VehicleID).HasColumnName("VehicleID");

                entity.Property(e => e.VehicleTypeID).HasColumnName("VehicleTypeID");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Requisitions)
                    .HasForeignKey(d => d.VehicleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requisitions_Vehicles");


                entity.HasOne(d => d.driverInfos)
                    .WithMany(p => p.requisitions)
                    .HasForeignKey(d => d.DriverID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired(false)
                    .HasConstraintName("FK_Requisitions_DriverInfos");

                entity.Ignore(b => b.VehicleName);
                entity.Ignore(b => b.VehicleTypeName);
                entity.Ignore(b => b.PageIndex);
                entity.Ignore(b => b.PageSize);
                entity.Ignore(b => b.DriverName);


            });

            modelBuilder.Entity<SessionLog>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Browser)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Device)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IsLoggedIn).HasColumnName("isLoggedIn");

                entity.Property(e => e.LoginDate).HasColumnType("datetime");

                entity.Property(e => e.LogoutDate).HasColumnType("datetime");

                entity.Property(e => e.OS)
                    .HasColumnName("OS")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionLog_Users");
            });

            modelBuilder.Entity<TravelDetails>(entity =>
            {
                entity.Property(p => p.TravelDetailId).ValueGeneratedOnAdd();
                entity.HasKey(e => e.TravelDetailId);

                entity.Property(e => e.TravelDetailId).HasColumnName("TravelDetailID");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.TravelDetails)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TravelDetails_DriverInfos");

                entity.HasOne(d => d.Requisition)
                    .WithMany(p => p.TravelDetails)
                    .HasForeignKey(d => d.RequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TravelDetails_Requisitions");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.TravelDetails)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TravelDetails_Vehicles");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.Property(p => p.UnitId).ValueGeneratedOnAdd();
                entity.HasKey(e => e.UnitId);

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Units)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Units_Departments");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.HasIndex(e => e.AccessRightId);

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccessRight)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccessRightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_AccessRight");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Users_Employees");
            });

            modelBuilder.Entity<VehicleDocuments>(entity =>
            {
                entity.Property(p => p.VehicleDocumentId).ValueGeneratedOnAdd();
                entity.HasKey(e => e.VehicleDocumentId);

                entity.Property(e => e.VehicleDocumentId).HasColumnName("VehicleDocumentID");

                entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleDocuments)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleDocuments_Vehicles");
            });

            modelBuilder.Entity<Vehicles>(entity =>
            {
                entity.HasKey(e => e.VehicleID);
                entity.Property(p => p.VehicleID).ValueGeneratedOnAdd();
                entity.Property(e => e.VehicleID).HasColumnName("VehicleID");

                entity.Property(e => e.VehicleTypeID).HasColumnName("VehicleTypeID");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_VehicleTypes");
            });

            modelBuilder.Entity<VehicleTypes>(entity =>
            {
                entity.Property(p => p.VehicleTypeID).ValueGeneratedOnAdd();
                entity.HasKey(e => e.VehicleTypeID);

                entity.Property(e => e.VehicleTypeID).HasColumnName("VehicleTypeID");
            });
        }
    }
}
