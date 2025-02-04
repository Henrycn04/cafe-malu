namespace backend.Domain
{
    public class VentaModel
    {
        public int ID { get; set; }
        public int IDCliente { get; set; }
        public DateTime Fecha { get; set; }
        public byte Semana { get; set; }
        public int IDCafe { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioTotal { get; set; }
        public decimal Pago { get; set; }
        public decimal Saldo { get; set; }
        public bool Debe { get; set; }
        public decimal PesoTotal { get; set; }


    }
}
