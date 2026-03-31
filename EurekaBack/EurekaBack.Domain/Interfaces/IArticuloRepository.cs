using EurekaBack.Domain.Entities;

namespace EurekaBack.Domain.Interfaces
{
    public interface IArticuloRepository
    {
        Task<IEnumerable<Articulo>> GetAllAsync();
        Task<Articulo?> GetByIdAsync(int id);
        Task<Articulo> AddAsync(Articulo articulo);
        Task UpdateAsync(Articulo articulo);
        Task UpdateStockAsync(int articuloId, int cantidad);
    }
}
