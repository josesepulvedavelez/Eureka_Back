namespace EurekaBack.Domain.Entities
{
    public class Factura
    {
        public int FacturaId { get; set; }
        public string No { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
        public ICollection<FacturaDetalle>? lstFacturaDetalle { get; set; }
    }
}
