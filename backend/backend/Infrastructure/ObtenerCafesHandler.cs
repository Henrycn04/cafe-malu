using backend.Domain;
using System.Data;
using System.Data.SqlClient;

namespace backend.Infrastructure
{
    public interface IObtenerCafe
    {
        ListaCafesModel ObtenerCafes();
    }
    public class ObtenerCafesHandler : IObtenerCafe
    {
        private SqlConnection _connection;
        private string _routeConnection;

        public ObtenerCafesHandler()
        {
            var builder = WebApplication.CreateBuilder();
            _routeConnection = builder.Configuration.GetConnectionString("CafeMaluContext");
            _connection = new SqlConnection(_routeConnection);
        }

        public ListaCafesModel ObtenerCafes()
        {
            List<CafeModel> listaCafes = new List<CafeModel>();
            string query = @"SELECT * FROM Cafe";

            try
            {
                _connection.Open();
                using (SqlCommand commandForQuery = new SqlCommand(query, _connection))
                using (SqlDataReader reader = commandForQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cafe = new CafeModel
                        {
                            ID = reader.GetInt32("ID"),
                            Nombre = reader.GetString("Nombre"),
                            Peso = (double)reader.GetDecimal("Peso"),
                            Especialidad = reader.GetBoolean("Especialidad"),
                            PrecioUnitario = (double)reader.GetDecimal("PrecioUnitario")
                        };
                        listaCafes.Add(cafe);
                    }
                }
            }
            finally
            {
                _connection.Close();
            }

            return new ListaCafesModel { Cafes = listaCafes };
        }
    }
}
