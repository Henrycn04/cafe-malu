using backend.Domain;
using System.Data;
using System.Data.SqlClient;

namespace backend.Infrastructure
{
    public interface IObtenerVentas
    {
        ListaVentasModel ObtenerVentas();
    }
    public class ObtenerVentasHandler : IObtenerVentas
    {
        private SqlConnection _connection;
        private string _routeConnection;

        public ObtenerVentasHandler()
        {
            var builder = WebApplication.CreateBuilder();
            _routeConnection = builder.Configuration.GetConnectionString("CafeMaluContext");
            _connection = new SqlConnection(_routeConnection);
        }

        public ListaVentasModel ObtenerVentas()
        {
            List<VentaModel> listasVentas = new List<VentaModel>();
            string query = @"SELECT * FROM Ventas";

            try
            {
                _connection.Open();
                using (SqlCommand commandForQuery = new SqlCommand(query, _connection))
                using (SqlDataReader reader = commandForQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var venta = new VentaModel
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            IDCliente = reader.GetInt32(reader.GetOrdinal("IDCliente")),
                            Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                            Semana = reader.GetByte(reader.GetOrdinal("Semana")),
                            IDCafe = reader.GetInt32(reader.GetOrdinal("IDCafe")),
                            Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                            PrecioTotal = reader.GetDecimal(reader.GetOrdinal("PrecioTotal")),
                            Pago = reader.GetDecimal(reader.GetOrdinal("Pago")),
                            Saldo = reader.GetDecimal(reader.GetOrdinal("Saldo")),
                            Debe = reader.GetBoolean(reader.GetOrdinal("Debe")),
                            PesoTotal = reader.GetDecimal(reader.GetOrdinal("PesoTotal"))
                        };
                        listasVentas.Add(venta);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las ventas: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return new ListaVentasModel { Ventas = listasVentas };
        }

    }
}
