using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EurekaBack.Models
{
    [Table("Factura")]
    public class Factura
    {
        public string No { get; set; } 
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [Key]
        public int FacturaId { get; set; }

        public List<FacturaDetalle>? lstFacturaDetalle { get; set; } 
    }
}
