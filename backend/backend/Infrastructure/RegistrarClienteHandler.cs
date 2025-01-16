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
            string query = @"INSERT INTO Clientes (Nombre, ClienteBusqueda, Frecuencia, NumeroTelefonico) 
                     VALUES (@Nombre, @ClienteBusqueda, @Frecuencia, @NumeroTelefonico)";

            try
            {
                _connection.Open();
                using (SqlCommand commandForQuery = new SqlCommand(query, _connection))
                {
                    commandForQuery.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    commandForQuery.Parameters.AddWithValue("@ClienteBusqueda", cliente.ClienteBusqueda);
                    commandForQuery.Parameters.AddWithValue("@Frecuencia", cliente.Frecuencia);
                    commandForQuery.Parameters.AddWithValue("@NumeroTelefonico", cliente.NumeroTelefonico);

                    commandForQuery.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.Close();
            }
            return 1;
        }
    }
}
