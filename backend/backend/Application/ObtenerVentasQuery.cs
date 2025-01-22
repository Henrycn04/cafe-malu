using backend.Infrastructure;
using backend.Domain;

namespace backend.Application
{
    public class ObtenerVentasQuery
    {
        private IObtenerVentas _IObtenerVentas;

        public ObtenerVentasQuery(IObtenerVentas IObtenerVentas)
        {
            this._IObtenerVentas = IObtenerVentas;
        }

        public ListaVentasModel ObtenerVentas()
        {
            ListaVentasModel ventas = new ListaVentasModel();
            ventas = this._IObtenerVentas.ObtenerVentas();
            return ventas;
        }
    }
}
