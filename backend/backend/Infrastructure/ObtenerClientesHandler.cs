using backend.Domain;
using System.Data;
using System.Data.SqlClient;

namespace backend.Infrastructure
{
    public interface IObtenerClientes
    {
        ListaClientesModel ObtenerClientes();
    }
    public class ObtenerClientesHandler : IObtenerClientes
    {
        private SqlConnection _connection;
        private string _routeConnection;

        public ObtenerClientesHandler()
        {
            var builder = WebApplication.CreateBuilder();
            _routeConnection = builder.Configuration.GetConnectionString("CafeMaluContext");
            _connection = new SqlConnection(_routeConnection);
        }

        public ListaClientesModel ObtenerClientes()
        {
            List<ClienteModel> listaClientes = new List<ClienteModel>();
            string query = @"SELECT * FROM Clientes";

            try
            {
                _connection.Open();
                using (SqlCommand commandForQuery = new SqlCommand(query, _connection))
                using (SqlDataReader reader = commandForQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cliente = new ClienteModel
                        {
                            ID = reader.GetInt32("ID"),
                            Nombre = reader.GetString("Nombre"),
                            ClienteBusqueda = reader.GetString("ClienteBusqueda"),
                            Frecuencia = (int)reader.GetInt32("Frecuencia"),
                            NumeroTelefonico = reader.GetString("NumeroTelefonico")
                        };
                        listaClientes.Add(cliente);
                    }
                }
            }
            finally
            {
                _connection.Close();
            }

            return new ListaClientesModel { Clientes = listaClientes };
        }
    }
}
