using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDLL.Domain
{
    public class Factura
    {
        public int Nro_factura { get; set; }
        public DateTime Fecha { get; set; }

        public Forma_pago Forma_pago { get; set; }

        public string Cliente { get; set; }



        public Factura(int nro_factura, DateTime fecha, Forma_pago forma_pago, string cliente)
        {
            Nro_factura = nro_factura;
            Fecha = fecha;
            Forma_pago = forma_pago;
            Cliente = cliente;
        }


        public Factura()
        {
        }
    }
}
