namespace Order_In_Stores.Models
{
    public class Address
    {
        public int Id { get; set; }           // ID adresu
        public string Street { get; set; }    // Ulica
        public string City { get; set; }      // Miasto
        public string PostalCode { get; set; } // Kod pocztowy

        // Relacja z zamówieniami
        public ICollection<Order> Orders { get; set; }
    }
}
