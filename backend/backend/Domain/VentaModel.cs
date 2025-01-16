namespace backend.Domain
{
    public class VentaModel
    {
        public int ID { get; set; }
        public int IDCliente { get; set; }
        public string Fecha { get; set; }
        public int Semana { get; set; }
        public int IDCafe { get; set; }
        public int Cantidad { get; set; }
        public double PrecioTotal { get; set; }
        public double Pago { get; set; }
        public double Saldo { get; set; }
        public bool Debe { get; set; }
        public double PesoTotal { get; set; }


    }
}
