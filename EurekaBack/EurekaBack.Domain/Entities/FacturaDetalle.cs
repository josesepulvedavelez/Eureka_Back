namespace EurekaBack.Domain.Entities
{
    public class FacturaDetalle
    {
        public int FacturaDetalleId { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public int ArticuloId { get; set; }
        public int FacturaId { get; set; }

        public Articulo? Articulo { get; set; }
        public Factura? Factura { get; set; }
    }
}
