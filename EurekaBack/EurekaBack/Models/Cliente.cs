using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EurekaBack.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        public string Cc_Nit { get; set; }
        public string Nombre_RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }

        [Key]
        public int ClienteId { get; set; }
    }
}
