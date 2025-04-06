namespace productsrad_bc.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Username { get; set; } = "";
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; } = true;
        // Otros campos según sea necesario

    }
}
