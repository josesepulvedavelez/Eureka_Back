using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EurekaBack.Models
{
	[Table("FacturaDetalle")]
	public class FacturaDetalle
	{
		public decimal Precio { get; set; }
		public int Cantidad { get; set; }
		public decimal SubTotal { get; set; }

		[ForeignKey("Articulo")]
		public int ArticuloId { get; set; }

		[ForeignKey("Factura")]
		public int FacturaId { get; set; }

		[Key]
		public int FacturaDetalleId { get; set; }
	}
}
