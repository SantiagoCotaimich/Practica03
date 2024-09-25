using DataDLL.Domain;
using DataDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDLL.Data
{
    public class FacturaRepositorio : IFacturaRepository
    {
        private readonly DataHelper helper;
        public FacturaRepositorio()
        {
            helper = DataHelper.GetInstance();
        }
        private List<Factura> TableToList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) { return null; }
            List<Factura> lst = new List<Factura>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(LoadFactura(row));
            }
            return lst;
        }
        private Factura LoadFactura(DataRow row)
        {
            int id = Convert.ToInt32(row["nro_factura"]);
            DateTime fecha = Convert.ToDateTime(row["fecha"]);
            Forma_pago forma_pago = new Forma_pago
            {
                Id_pago = Convert.ToInt32(row["id_forma_pago"]),
                Nombre = row["nombre"].ToString()
            };
            string cliente = row["cliente"].ToString();
            return new Factura(id, fecha, forma_pago, cliente);
        }

        public bool Delete(int id)
        {
            bool result = false;
            List<Parameter> parametros = new List<Parameter>() { new Parameter("@nro_factura", id) };
            result = (1 == helper.ExecuteSPNonQuery("BORRAR_FACTURAS", parametros));
            return result;
        }


        public List<Factura> GetAll()
        {
            return TableToList(helper.ExecuteSPQuery("OBTENER_FACTURAS_TODAS", null));
        }


        public bool Save(Factura oFactura)
        {
            List<Parameter> parametros = new List<Parameter>
            {
                new Parameter("@nro_factura", oFactura.Nro_factura),
                new Parameter("@fecha", oFactura.Fecha ),
                new Parameter("@id_forma_pago", oFactura.Forma_pago.Id_pago),
                new Parameter("cliente", oFactura.Cliente)
            };
            int rows = helper.ExecuteSPNonQuery("GUARDAR_FACTURA", parametros);
            return rows == 1;
        }

        public bool ExistFormaPago(int idFormaPago)
        {
            List<Parameter> parametros = new List<Parameter>
    {
        new Parameter("@Id_pago", idFormaPago)
    };

            DataTable result = helper.ExecuteSPQuery("EXISTE_FORMA_PAGO", parametros);

            if (result != null && result.Rows.Count > 0)
            {
                return Convert.ToInt32(result.Rows[0]["ExistsFlag"]) == 1;
            }

            return false;
        }

        public List<Factura> GetDatePay(DateTime? fecha, string? nombre)
        {
            var lstParams = new List<Parameter>

    {
        new Parameter("@fecha", fecha),
        new Parameter("@nombre", nombre)
    };

            DataTable tabla = helper.ExecuteSPQuery("OBTENER_FACTURAS_FECHA_PAGO", lstParams);

            List<Factura> lista = new List<Factura>();

            foreach (DataRow dr in tabla.Rows)
            {
                Factura factura = LoadFactura(dr);
                lista.Add(factura);
            }

            return lista;
        }


    }


}
