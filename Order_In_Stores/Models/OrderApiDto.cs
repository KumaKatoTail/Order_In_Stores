namespace Order_In_Stores.Models
{
    public class OrderApiDto
    {
        public int Id { get; set; }          // ID zamówienia
        public string StoreName { get; set; } // Nazwa sklepu
        public string City { get; set; }     // Miasto
        public decimal NetValue { get; set; } // Suma wartości netto
    }
}
