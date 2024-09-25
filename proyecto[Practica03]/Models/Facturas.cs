using DataDLL.Domain;

namespace proyecto_Practica03_.Models

{
    public class Facturas
    {
        public int Nro_factura { get; set; }
        public DateTime Fecha { get; set; }

        public Forma_pago forma_pago { get; set; }

        public string Cliente { get; set; }

    }


}
