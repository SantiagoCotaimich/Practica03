using DataDLL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDLL.Interfaces
{
    public interface IFacturaRepository
    {
        List<Factura> GetAll();

        List<Factura> GetDatePay(DateTime? fecha, string? nombre);
        bool Save(Factura oFactura);
        bool Delete(int id);
        public bool ExistFormaPago(int idFormaPago);

    }
}
