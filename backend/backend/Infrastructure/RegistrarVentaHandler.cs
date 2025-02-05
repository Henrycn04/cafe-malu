﻿using backend.Domain;
using System.Data;
using System.Data.SqlClient;

namespace backend.Infrastructure
{

    public interface IRegistrarVenta
    {
        int RegistrarVenta(VentaModel venta);
        decimal ObtenerPrecioCafe(int IDCafe);
        decimal ObtenerPesoCafe(int IDCafe);
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
            int filas = 0;
            string query = @"INSERT INTO Ventas (IDCliente, Fecha, Semana, IDCafe, Cantidad, PrecioTotal, Pago, Saldo, Debe, PesoTotal) 
                     VALUES (@IDCliente, @Fecha, @Semana, @IDCafe, @Cantidad, @PrecioTotal, @Pago, @Saldo, @Debe, @PesoTotal)";

            try
            {
                _connection.Open();
                using (SqlCommand commandForQuery = new SqlCommand(query, _connection))
                {
                    commandForQuery.Parameters.AddWithValue("@IDCliente", venta.IDCliente);
                    commandForQuery.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    commandForQuery.Parameters.AddWithValue("@Semana", venta.Semana);
                    commandForQuery.Parameters.AddWithValue("@IDCafe", venta.IDCafe);
                    commandForQuery.Parameters.AddWithValue("@Cantidad", venta.Cantidad);
                    commandForQuery.Parameters.AddWithValue("@PrecioTotal", venta.PrecioTotal);
                    commandForQuery.Parameters.AddWithValue("@Pago", venta.Pago);
                    commandForQuery.Parameters.AddWithValue("@Saldo", venta.Saldo);
                    commandForQuery.Parameters.AddWithValue("@Debe", venta.Debe);
                    commandForQuery.Parameters.AddWithValue("@PesoTotal", venta.PesoTotal);


                    filas = commandForQuery.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.Close();
            }
            return filas;
        }
        public decimal ObtenerPrecioCafe(int IDCafe)
        {
            decimal precio = 0.0m;
            string query = @"
        SELECT PrecioUnitario FROM dbo.Cafe
        WHERE ID = @IDCafe";

            try
            {
                _connection.Open();
                using (SqlCommand commandForQuery = new SqlCommand(query, _connection))
                {
                    commandForQuery.Parameters.AddWithValue("@IDCafe", IDCafe);
                    var result = commandForQuery.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        precio = (decimal)result;  // Cast a decimal
                    }
                }
            }
            finally
            {
                _connection.Close();
            }
            return precio;
        }



        public decimal ObtenerPesoCafe(int IDCafe)
        {
            decimal peso = 0;
            string query = @"
                SELECT Peso FROM dbo.Cafe
                WHERE ID = @IDCafe";
            try
            {
                _connection.Open();
                using (SqlCommand commandForQuery = new SqlCommand(query, _connection))
                {
                    commandForQuery.Parameters.AddWithValue("@IDCafe", IDCafe);
                    var result = commandForQuery.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        peso = (decimal)result;  // Cast a decimal
                    }
                }
            }
            finally
            {
                _connection.Close();
            }
            return peso;
        }
    }
}
