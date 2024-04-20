using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EurekaBack.Dto
{
    public class FacturaDetalleDto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }        
        public int ArticuloId { get; set; }

        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }        
        public int FacturaId { get; set; }       
        public int FacturaDetalleId { get; set; }

    }
}
