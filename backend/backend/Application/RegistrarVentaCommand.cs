using backend.Domain;
using backend.Infrastructure;

namespace backend.Application
{
    public class RegistrarVentaCommand
    {
        private IRegistrarVenta _IRegistrarVenta;

        public RegistrarVentaCommand(IRegistrarVenta IRegistrarVenta) 
        {
            this._IRegistrarVenta = IRegistrarVenta;
        }

        public int RegistrarVenta(VentaModel venta)
        {
            int filas = 0;
            filas = this._IRegistrarVenta.RegistrarVenta(venta);
            return filas;
        }
    }
}
