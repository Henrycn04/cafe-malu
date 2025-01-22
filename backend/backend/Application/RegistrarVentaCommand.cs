using backend.Domain;
using backend.Infrastructure;
using System.Globalization;

namespace backend.Application
{
    public class RegistrarVentaCommand
    {
        private IRegistrarVenta _IRegistrarVenta;

        public RegistrarVentaCommand(IRegistrarVenta IRegistrarVenta) 
        {
            this._IRegistrarVenta = IRegistrarVenta;
        }

        public int RegistrarVenta(VentaModel venta)
        {
            int filas = 0;
            if (CalcularPrecioyPesoTotales(venta))
            {
                CalcularSemana(venta);
                CalcularSaldo(venta);
                filas = this._IRegistrarVenta.RegistrarVenta(venta);
            }
            return filas;
        }

        private void CalcularSemana(VentaModel venta)
        {
            CultureInfo cultura = CultureInfo.CurrentCulture;
            Calendar calendario = cultura.Calendar;
            CalendarWeekRule reglaSemana = cultura.DateTimeFormat.CalendarWeekRule;
            DayOfWeek primerDiaSemana = cultura.DateTimeFormat.FirstDayOfWeek;
            int semana = calendario.GetWeekOfYear(venta.Fecha, reglaSemana, primerDiaSemana);
            venta.Semana = semana;
        }

        private bool CalcularPrecioyPesoTotales(VentaModel venta)
        {
            double precioUnitarioCafe = 0.0;
            double pesoUnitarioCafe = 0.0;
            precioUnitarioCafe = this._IRegistrarVenta.ObtenerPrecioCafe(venta.IDCafe);
            pesoUnitarioCafe = this._IRegistrarVenta.ObtenerPesoCafe(venta.IDCafe);
            if (precioUnitarioCafe != 0.0 && pesoUnitarioCafe != 0.0)
            {
                venta.PesoTotal = pesoUnitarioCafe * venta.Cantidad;
                venta.PrecioTotal = precioUnitarioCafe * venta.Cantidad;
                return true;
            }
            return false;
        }

        private void CalcularSaldo(VentaModel venta)
        {
            venta.Saldo = venta.PrecioTotal - venta.Pago;
            if (venta.Saldo != 0)
            {
                venta.Debe = true;
            }
        }
    }
}
