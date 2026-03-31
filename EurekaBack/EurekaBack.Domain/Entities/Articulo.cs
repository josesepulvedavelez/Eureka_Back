namespace EurekaBack.Domain.Entities
{
    public class Articulo
    {
        public int ArticuloId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Costo { get; set; }
        public double Porcentaje { get; set; }
        public decimal PrecioSugerido { get; set; }
        public int Cantidad { get; set; }
        public bool Estado { get; set; }

        public ICollection<FacturaDetalle>? FacturaDetalles { get; set; }
    }
}
