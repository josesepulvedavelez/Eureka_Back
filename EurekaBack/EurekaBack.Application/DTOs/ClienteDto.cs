namespace EurekaBack.Application.DTOs
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        public string Cc_Nit { get; set; } = string.Empty;
        public string Nombre_RazonSocial { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
