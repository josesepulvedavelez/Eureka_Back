using EurekaBack.Models;
using Microsoft.EntityFrameworkCore;

namespace EurekaBack.Data
{
    public class EurekaContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalle { get; set; }

        public EurekaContext(DbContextOptions<EurekaContext> options) : base(options) 
        { 
        
        }

    }
}
