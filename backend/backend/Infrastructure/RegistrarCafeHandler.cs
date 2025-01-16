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
            return filas;
        }
    }
}
