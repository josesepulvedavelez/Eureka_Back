using EurekaBack.Domain.Entities;
using EurekaBack.Domain.Interfaces;
using EurekaBack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EurekaBack.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly EurekaContext _context;

        public ClienteRepository(EurekaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            return cliente;
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
        }
    }
}
