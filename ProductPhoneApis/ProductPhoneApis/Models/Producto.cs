using System.Collections;

namespace ProductPhoneApis.Models
{
    public class Producto
    {
        public int? ID_Producto { get; set; }
        public string Nombre { get; set; }
        public int? ID_Color { get; set; }
        public string Color { get; set; }
        public double Precio { get; set; }
        public DateTime? Fecha_Ultima_Mod { get; set; }
        public int Cantidad { get; set; }
        public BitArray image { get; set; }
    }
}
