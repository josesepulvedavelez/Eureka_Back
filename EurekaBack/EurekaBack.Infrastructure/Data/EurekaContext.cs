using EurekaBack.Application.Interfaces;
using EurekaBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EurekaBack.Infrastructure.Data
{
    public class EurekaContext : DbContext, IApplicationDbContext
    {
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }

        public EurekaContext(DbContextOptions<EurekaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.ToTable("Articulo");
                entity.HasKey(e => e.ArticuloId);
                entity.Property(e => e.ArticuloId).ValueGeneratedOnAdd();
                entity.Property(e => e.Codigo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Costo).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Porcentaje).HasColumnType("float");
                entity.Property(e => e.PrecioSugerido).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(e => e.ClienteId);
                entity.Property(e => e.ClienteId).ValueGeneratedOnAdd();
                entity.Property(e => e.Cc_Nit).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Nombre_RazonSocial).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Direccion).HasMaxLength(200);
                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");
                entity.HasKey(e => e.FacturaId);
                entity.Property(e => e.FacturaId).ValueGeneratedOnAdd();
                entity.Property(e => e.No).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.Facturas)
                    .HasForeignKey(e => e.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FacturaDetalle>(entity =>
            {
                entity.ToTable("FacturaDetalle");
                entity.HasKey(e => e.FacturaDetalleId);
                entity.Property(e => e.FacturaDetalleId).ValueGeneratedOnAdd();
                entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SubTotal).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Articulo)
                    .WithMany(a => a.FacturaDetalles)
                    .HasForeignKey(e => e.ArticuloId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Factura)
                    .WithMany(f => f.lstFacturaDetalle)
                    .HasForeignKey(e => e.FacturaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
