using Microsoft.EntityFrameworkCore;

namespace M000_DataAccess;

public partial class KursDBContext : DbContext
{
    public KursDBContext() { }

    public KursDBContext(DbContextOptions<KursDBContext> options) : base(options) { }

    public virtual DbSet<KursInhalte> KursInhalte { get; set; }

    public virtual DbSet<Kurse> Kurse { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KursInhalte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KursInha__3214EC2792AD7B25");

            entity.HasOne(d => d.Kurs).WithMany(p => p.KursInhalte).HasConstraintName("FK_KursID");
        });

        modelBuilder.Entity<Kurse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kurse__3214EC27A6E3F9C0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}