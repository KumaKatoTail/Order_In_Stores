namespace Order_In_Stores.Models
{
    public class OrderLineDto
    {
        public string ProductCode { get; set; }
        public decimal NetPrice { get; set; }
        public decimal GrossPrice { get; set; }
        public int Quantity { get; set; }
    }
}
