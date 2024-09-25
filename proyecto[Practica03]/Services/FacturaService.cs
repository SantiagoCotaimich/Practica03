using proyecto_Practica03_.Models;
using DataDLL.Data;
using DataDLL.Domain;
using DataDLL.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace proyecto_Practica03_.Services
{
    public class FacturaService : IAplication
    {
        private readonly IFacturaRepository repositorio;
        public FacturaService()
        {
            repositorio = new FacturaRepositorio();
        }

        public bool AgregarFactura(Facturas fac)
        {
            Factura factura = Mapper.Set(fac);
            return repositorio.Save(factura);
        }

        public bool BorrarFactura(int id)
        {
            return repositorio.Delete(id);
        }
        public List<Facturas> GetFactura()
        {
            var lstFactura = repositorio.GetAll();
            return Mapper.GetList(lstFactura);
        }


        public bool ActualizarFactura(int id, Facturas fac)
        {
            var factura = Mapper.Set(fac);
            fac.Nro_factura = id;
            return repositorio.Save(factura);
        }

        public bool ExistFormaPago(int idFormaPago)
        {
            return repositorio.ExistFormaPago(idFormaPago);
        }

        public List<Factura> GetFacturaFechaPago(DateTime? fecha, string? nombre)
        {
            return repositorio.GetDatePay(fecha, nombre);
        }
    }
}
