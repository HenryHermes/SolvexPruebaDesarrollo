namespace ProductPhoneApis.Models
{
    public class Carrito
    {
        public int ID_Carrito { get; set; }
        public int ID_Usuario { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Ultima_Mod { get; set; }
        public List<Producto> Productos_Carrito { get; set; }
    }
}
