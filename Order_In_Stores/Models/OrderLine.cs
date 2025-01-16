namespace Order_In_Stores.Models
{
    public class OrderLine
    {
        public int Id { get; set; }           // ID linii zamówienia
        public string ProductCode { get; set; } // Kod produktu
        public decimal NetPrice { get; set; }  // Cena netto
        public decimal GrossPrice { get; set; } // Cena brutto
        public int Quantity { get; set; }      // Ilość sztuk
        public int OrderId { get; set; }       // Klucz obcy do zamówienia
    }
}
