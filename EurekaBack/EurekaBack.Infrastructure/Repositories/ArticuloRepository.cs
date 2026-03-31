using EurekaBack.Domain.Entities;
using EurekaBack.Domain.Interfaces;
using EurekaBack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EurekaBack.Infrastructure.Repositories
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly EurekaContext _context;

        public ArticuloRepository(EurekaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Articulo>> GetAllAsync()
        {
            return await _context.Articulos.ToListAsync();
        }

        public async Task<Articulo?> GetByIdAsync(int id)
        {
            return await _context.Articulos.FindAsync(id);
        }

        public async Task<Articulo> AddAsync(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            return articulo;
        }

        public async Task UpdateAsync(Articulo articulo)
        {
            _context.Entry(articulo).State = EntityState.Modified;
        }

        public async Task UpdateStockAsync(int articuloId, int cantidad)
        {
            var articulo = await _context.Articulos.FindAsync(articuloId);
            if (articulo != null)
            {
                articulo.Cantidad -= cantidad;
            }
        }
    }
}
