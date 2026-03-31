namespace EurekaBack.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class NotFoundException : DomainException
    {
        public NotFoundException(string entityName, object key)
            : base($"Entity \"{entityName}\" ({key}) was not found.") { }
    }

    public class InsufficientStockException : DomainException
    {
        public InsufficientStockException(int articuloId, int requested, int available)
            : base($"Insufficient stock for Article {articuloId}. Requested: {requested}, Available: {available}") { }
    }

    public class ValidationException : DomainException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("One or more validation failures have occurred.")
        {
            Errors = errors;
        }
    }
}
