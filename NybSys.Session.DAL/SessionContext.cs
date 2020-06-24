using Microsoft.EntityFrameworkCore;

namespace NybSys.Session.DAL
{
    public partial class SessionContext : DbContext
    {
        public SessionContext()
        {
        }

        public SessionContext(DbContextOptions<SessionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SessionDTO> Session { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Session.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionDTO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
    }
}
