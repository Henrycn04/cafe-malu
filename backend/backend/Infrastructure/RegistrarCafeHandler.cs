using backend.Domain;
using System.Data;
using System.Data.SqlClient;

namespace backend.Infrastructure
{

    public interface IRegistrarCafe
    {
        int RegistrarCafe(CafeModel cafe);
    }
    public class RegistrarCafeHandler : IRegistrarCafe
    {
        private SqlConnection _connection;
        private string _routeConnection;

        public RegistrarCafeHandler()
        {
            var builder = WebApplication.CreateBuilder();
            _routeConnection = builder.Configuration.GetConnectionString("CafeMaluContext");
            _connection = new SqlConnection(_routeConnection);
        }

        public int RegistrarCafe(CafeModel cafe)
        {
            int filas = 0;
            string query = @"INSERT INTO Cafe (Nombre, Peso, Especialidad, PrecioUnitario) 
                     VALUES (@Nombre, @Peso, @Especialidad, @PrecioUnitario)";

            try
            {
                _connection.Open();
                using (SqlCommand commandForQuery = new SqlCommand(query, _connection))
                {
                    commandForQuery.Parameters.AddWithValue("@Nombre", cafe.Nombre);
                    commandForQuery.Parameters.AddWithValue("@Peso", cafe.Peso);
                    commandForQuery.Parameters.AddWithValue("@Especialidad", cafe.Especialidad);
                    commandForQuery.Parameters.AddWithValue("@PrecioUnitario", cafe.PrecioUnitario);

                    filas = commandForQuery.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.Close();
            }
            return filas;
        }
    }
}
