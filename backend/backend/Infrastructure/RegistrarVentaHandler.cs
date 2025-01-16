using backend.Domain;
using System.Data;
using System.Data.SqlClient;

namespace backend.Infrastructure
{

    public interface IRegistrarVenta
    {
        int RegistrarVenta(VentaModel venta);
    }
    public class RegistrarVentaHandler : IRegistrarVenta
    {
        private SqlConnection _connection;
        private string _routeConnection;

        public RegistrarVentaHandler()
        {
            var builder = WebApplication.CreateBuilder();
            _routeConnection = builder.Configuration.GetConnectionString("CafeMaluContext");
            _connection = new SqlConnection(_routeConnection);
        }

        public int RegistrarVenta(VentaModel venta)
        {
            Console.WriteLine(venta.Cantidad);
            int filas = 0;
            return filas;
        }
    }
}
