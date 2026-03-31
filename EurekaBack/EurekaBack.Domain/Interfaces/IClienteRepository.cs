using EurekaBack.Domain.Entities;

namespace EurekaBack.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<Cliente> AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
    }
}
