using CvecaraFlora.Modeli.Modeli;
using Microsoft.EntityFrameworkCore;

namespace CvecaraFlora.Repozitorijum
{
    public class CvecaraFloraDbContext : DbContext
    {
        public DbSet<TipAranzmana> TipoviAranzmana { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Konekcija.VratiKonekcioniString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipAranzmana>().ToTable("TipAranzmana");
            modelBuilder.Entity<TipAranzmana>().HasKey(t => t.TipAranzmanaID);
        }
    }
}