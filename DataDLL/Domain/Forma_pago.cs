using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDLL.Domain
{
    public class Forma_pago
    {
        public int Id_pago { get; set; }
        public string Nombre { get; set; }



        public Forma_pago()
        {
            Id_pago = 0;
            Nombre = null;
        }

        public Forma_pago(int id, string nombre)
        {
            Id_pago = id;
            Nombre = nombre;
        }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
