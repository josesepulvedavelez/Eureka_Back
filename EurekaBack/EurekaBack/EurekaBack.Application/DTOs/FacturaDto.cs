namespace EurekaBack.Application.DTOs
{
    public class FacturaDto
    {
        public string? Cc_Nit { get; set; }
        public string? Nombre_RazonSocial { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool? Estado { get; set; }
        public int ClienteId { get; set; }
        public string No { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<FacturaDetalleDto> lstFacturaDetalleDto { get; set; } = new();
        public int FacturaId { get; set; }
    }
}
