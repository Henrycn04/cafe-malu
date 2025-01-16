using backend.Domain;
using System.Data;
using System.Data.SqlClient;

namespace backend.Infrastructure
{

    public interface IRegistrarCliente
    {
        int RegistrarCliente(ClienteModel cliente);
    }
    public class RegistrarClienteHandler : IRegistrarCliente
    {
        private SqlConnection _connection;
        private string _routeConnection;

        public RegistrarClienteHandler()
        {
            var builder = WebApplication.CreateBuilder();
            _routeConnection = builder.Configuration.GetConnectionString("CafeMaluContext");
            _connection = new SqlConnection(_routeConnection);
        }

        public int RegistrarCliente(ClienteModel cliente)
        {
            int filas = 0;
            return filas;
        }
    }
}
