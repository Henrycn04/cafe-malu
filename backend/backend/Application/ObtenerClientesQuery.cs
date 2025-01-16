using backend.Infrastructure;
using backend.Domain;

namespace backend.Application
{
    public class ObtenerClientesQuery
    {
        private IObtenerClientes _IObtenerClientes;

        public ObtenerClientesQuery(IObtenerClientes IObtenerClientes)
        {
            this._IObtenerClientes = IObtenerClientes;
        }

        public ListaClientesModel ObtenerClientes()
        {
            ListaClientesModel clientes = new ListaClientesModel();
            clientes = this._IObtenerClientes.ObtenerClientes();
            return clientes;
        }
    }
}
