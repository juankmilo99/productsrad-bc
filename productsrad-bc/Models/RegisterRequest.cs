namespace productsrad_bc.Models
{
    public class RegisterRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Username { get; set; } = "";
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; } = true;
        
    }
}
