using EurekaBack.Domain.Entities;
using EurekaBack.Domain.Interfaces;
using EurekaBack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EurekaBack.Infrastructure.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly EurekaContext _context;

        public FacturaRepository(EurekaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Factura>> GetAllAsync()
        {
            return await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.lstFacturaDetalle)
                    .ThenInclude(d => d.Articulo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Factura>> GetByDateRangeAsync(DateTime fechaInicial, DateTime fechaFinal)
        {
            return await _context.Facturas
                .Include(f => f.Cliente)
                .Where(f => f.Fecha >= fechaInicial && f.Fecha <= fechaFinal)
                .ToListAsync();
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(f => f.FacturaId == id);
        }

        public async Task<Factura?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.lstFacturaDetalle)
                    .ThenInclude(d => d.Articulo)
                .FirstOrDefaultAsync(f => f.FacturaId == id);
        }

        public async Task<Factura> AddAsync(Factura factura)
        {
            _context.Facturas.Add(factura);
            return factura;
        }
    }
}
