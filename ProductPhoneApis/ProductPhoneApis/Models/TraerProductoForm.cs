namespace ProductPhoneApis.Models
{
    public class TraerProductoForm
    {
        public int ID_Producto { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public int ID_Color { get; set; } = 0;
        public string Color { get; set; } = "";
        public double Precio1 { get; set; } = 0;
        public double Precio2 { get; set; } = 999999999.99;
        public DateTime? Fecha_Ultima_Mod_1 { get; set; }
        public DateTime? Fecha_Ultima_Mod_2 { get; set; }
        public long Cantidad1 { get; set; } = 0;
        public long Cantidad2 { get; set; } = 99999999999;

    }
}
