namespace EurekaBack.Application.DTOs
{
    public class CreateFacturaDto
    {
        public string No { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public List<CreateFacturaDetalleDto> lstFacturaDetalleDto { get; set; } = new();
    }

    public class CreateFacturaDetalleDto
    {
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
    }
}
