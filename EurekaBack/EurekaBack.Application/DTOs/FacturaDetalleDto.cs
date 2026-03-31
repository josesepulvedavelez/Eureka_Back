namespace EurekaBack.Application.DTOs
{
    public class FacturaDetalleDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int ArticuloId { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public int FacturaId { get; set; }
        public int FacturaDetalleId { get; set; }
    }
}
