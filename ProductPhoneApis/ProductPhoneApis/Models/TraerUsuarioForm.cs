namespace ProductPhoneApis.Models
{
    public class TraerUsuarioForm
    {
        public int ID_Usuario { get; set; } = 0;
        public string Username { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public int ID_Rol { get; set; } = 0;
        public string Rol { get; set; } = "";
    }
}
