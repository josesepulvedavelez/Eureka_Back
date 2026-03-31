using EurekaBack.Domain.Interfaces;
using EurekaBack.Infrastructure.Data;
using EurekaBack.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EurekaBack.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EurekaContext _context;
        private IDbContextTransaction? _transaction;

        private IArticuloRepository? _articulos;
        private IClienteRepository? _clientes;
        private IFacturaRepository? _facturas;

        public UnitOfWork(EurekaContext context)
        {
            _context = context;
        }

        public IArticuloRepository Articulos =>
            _articulos ??= new ArticuloRepository(_context);

        public IClienteRepository Clientes =>
            _clientes ??= new ClienteRepository(_context);

        public IFacturaRepository Facturas =>
            _facturas ??= new FacturaRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
