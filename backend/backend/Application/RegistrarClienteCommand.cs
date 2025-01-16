using backend.Domain;
using backend.Infrastructure;

namespace backend.Application
{
    public class RegistrarClienteCommand
    {
        private IRegistrarCliente _IRegistrarCliente;

        public RegistrarClienteCommand(IRegistrarCliente IRegistrarCliente)
        {
            this._IRegistrarCliente = IRegistrarCliente;
        }

        public int RegistrarCliente(ClienteModel cliente)
        {
            int filas = 0;
            filas = this._IRegistrarCliente.RegistrarCliente(cliente);
            return filas;
        }
    }
}
