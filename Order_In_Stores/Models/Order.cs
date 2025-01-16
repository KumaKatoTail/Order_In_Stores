namespace Order_In_Stores.Models
{
    public class Order
    {
        public int Id { get; set; }           // ID zamówienia
        public int StoreNumber { get; set; }  // Numer sklepu
        public string StoreName { get; set; } // Nazwa sklepu
        public PaymentType PaymentType { get; set; } // Typ płatności 

        // Klucz obcy do adresu klienta
        public int CustomerAddressId { get; set; }
        public Address CustomerAddress { get; set; }

        // Relacja do linii zamówienia
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
