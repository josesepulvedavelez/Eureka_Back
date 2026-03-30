namespace EurekaBack.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IArticuloRepository Articulos { get; }
        IClienteRepository Clientes { get; }
        IFacturaRepository Facturas { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
