using Microsoft.EntityFrameworkCore;
using NybSys.Models.DTO;

namespace NybSys.AuditLog.DAL
{

    public partial class AuditLogContext : DbContext
    {
        public AuditLogContext()
        {
        }

        public AuditLogContext(DbContextOptions<AuditLogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.DTO.Action> Action { get; set; }
        public virtual DbSet<LogMain> LogMain { get; set; }
        public virtual DbSet<LogType> LogType { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<LogWrite> LogWrite { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.DTO.Action>(entity =>
            {
                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.ActionName).HasMaxLength(50);
            });

            modelBuilder.Entity<LogMain>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CalledFunction)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FormName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LogDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                       .ValueGeneratedOnAdd();

                entity.Property(e => e.LogMessage).HasColumnType("text");

                entity.Property(e => e.LogTime).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.LogMain)
                    .HasForeignKey(d => d.ActionId)
                    .HasConstraintName("FK_LogMain_Action");

                entity.HasOne(d => d.LogType)
                    .WithMany(p => p.LogMain)
                    .HasForeignKey(d => d.LogTypeId)
                    .HasConstraintName("FK_LogMain_LogType");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.LogMain)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK_LogMain_Module");
            });

            modelBuilder.Entity<LogType>(entity =>
            {
                entity.Property(e => e.LogTypeId).HasColumnName("LogTypeID");

                entity.Property(e => e.LogTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.ModuleName).HasMaxLength(50);
            });
        }
    }
}
