using proyecto_Practica03_.Models;
using DataDLL.Domain;

namespace proyecto_Practica03_.Services
{
    public interface IAplication
    {
        List<Facturas> GetFactura();
        bool AgregarFactura(Facturas factura);
        bool ActualizarFactura(int id, Facturas facturaActualizada);
        bool BorrarFactura(int codigo);
        bool ExistFormaPago(int idFormaPago);
        public List<Factura> GetFacturaFechaPago(DateTime? fecha, string? nombre);
    }

}
