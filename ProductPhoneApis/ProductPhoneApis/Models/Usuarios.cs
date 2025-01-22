namespace ProductPhoneApis.Models
{
    public class Usuarios
    {
        public int ID_Usuarios { get; set; }

        public string Username { get; set; }
        
        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Contrase { get; set; }

        public DateTime Fecha_Ultima_Mod { get; set; }

        public int ID_Rol { get; set; }

        public string Rol { get; set; }
    }
}
