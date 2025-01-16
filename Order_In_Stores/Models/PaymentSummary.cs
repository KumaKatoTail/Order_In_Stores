namespace Order_In_Stores.Models
{
    public class PaymentSummary
    {
        public PaymentType PaymentType { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalGrossValue { get; set; }
    }
}

