using proyecto_Practica03_.Models;
using DataDLL.Domain;

namespace proyecto_Practica03_.Services
{
    public static class Mapper
    {
        public static Factura Set(Facturas fac)
        {
            var factura = new Factura()
            {
                Nro_factura = fac.Nro_factura,
                Fecha = fac.Fecha,
                Forma_pago = fac.forma_pago,
                Cliente = fac.Cliente,
            };
            return factura;
        }
        public static Facturas Get(Factura factura)
        {
            if (factura == null) { return null; }
            var dto = new Facturas()
            {
                Nro_factura = factura.Nro_factura,
                Fecha = factura.Fecha,
                forma_pago = factura.Forma_pago,
                Cliente = factura.Cliente,
            };
            return dto;
        }
        public static List<Facturas> GetList(List<Factura> facturas)
        {
            if (facturas == null || facturas.Count == 0) { return null; }
            List<Facturas> lstdto = new List<Facturas>();
            foreach (Factura factura in facturas)
            {
                lstdto.Add(Get(factura));
            }
            return lstdto;
        }

    }
}

