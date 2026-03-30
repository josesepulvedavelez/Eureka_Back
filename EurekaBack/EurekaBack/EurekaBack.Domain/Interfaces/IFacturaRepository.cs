using EurekaBack.Domain.Entities;

namespace EurekaBack.Domain.Interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> GetAllAsync();
        Task<IEnumerable<Factura>> GetByDateRangeAsync(DateTime fechaInicial, DateTime fechaFinal);
        Task<Factura?> GetByIdAsync(int id);
        Task<Factura?> GetByIdWithDetailsAsync(int id);
        Task<Factura> AddAsync(Factura factura);
    }
}
