using Microsoft.EntityFrameworkCore;
using Order_In_Stores.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    // DbSet'y odpowiadające tabelom w bazie danych
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relacja 
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderLines)
            .WithOne()
            .HasForeignKey(ol => ol.OrderId);

        // Relacja 
        modelBuilder.Entity<Order>()
            .HasOne(o => o.CustomerAddress)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.CustomerAddressId);

        // Określenie precyzji i skali 
        modelBuilder.Entity<OrderLine>()
            .Property(ol => ol.NetPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderLine>()
            .Property(ol => ol.GrossPrice)
            .HasColumnType("decimal(18,2)");

        // Seedowanie przykładowych danych
        modelBuilder.Entity<Address>().HasData(
            new Address { Id = 1, Street = "Ulica Kwiatowa 1", City = "Warszawa", PostalCode = "00-001" },
            new Address { Id = 2, Street = "Ulica Wiosenna 5", City = "Kraków", PostalCode = "30-001" },
            new Address { Id = 3, Street = "Ulica Zimowa 3", City = "Gdańsk", PostalCode = "80-001" },
            new Address { Id = 4, Street = "Ulica Letnia 8", City = "Wrocław", PostalCode = "50-001" },
            new Address { Id = 5, Street = "Ulica Jesienna 12", City = "Poznań", PostalCode = "60-001" }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, StoreNumber = 2, StoreName = "Sklep 2", PaymentType = PaymentType.Card, CustomerAddressId = 1 },
            new Order { Id = 2, StoreNumber = 4, StoreName = "Sklep 4", PaymentType = PaymentType.Cash, CustomerAddressId = 2 },
            new Order { Id = 3, StoreNumber = 6, StoreName = "Sklep 6", PaymentType = PaymentType.Transfer, CustomerAddressId = 3 },
            new Order { Id = 4, StoreNumber = 8, StoreName = "Sklep 8", PaymentType = PaymentType.Card, CustomerAddressId = 4 },
            new Order { Id = 5, StoreNumber = 10, StoreName = "Sklep 10", PaymentType = PaymentType.Cash, CustomerAddressId = 5 }
        );

        modelBuilder.Entity<OrderLine>().HasData(
            new OrderLine { Id = 1, ProductCode = "P001", NetPrice = 100m, GrossPrice = 123m, Quantity = 2, OrderId = 1 },
            new OrderLine { Id = 2, ProductCode = "P002", NetPrice = 50m, GrossPrice = 61.5m, Quantity = 3, OrderId = 1 },
            new OrderLine { Id = 3, ProductCode = "P003", NetPrice = 200m, GrossPrice = 246m, Quantity = 1, OrderId = 2 },
            new OrderLine { Id = 4, ProductCode = "P004", NetPrice = 80m, GrossPrice = 98.4m, Quantity = 2, OrderId = 2 },
            new OrderLine { Id = 5, ProductCode = "P005", NetPrice = 300m, GrossPrice = 369m, Quantity = 1, OrderId = 3 },
            new OrderLine { Id = 6, ProductCode = "P006", NetPrice = 60m, GrossPrice = 73.8m, Quantity = 5, OrderId = 3 },
            new OrderLine { Id = 7, ProductCode = "P007", NetPrice = 150m, GrossPrice = 184.5m, Quantity = 2, OrderId = 4 },
            new OrderLine { Id = 8, ProductCode = "P008", NetPrice = 90m, GrossPrice = 110.7m, Quantity = 3, OrderId = 4 },
            new OrderLine { Id = 9, ProductCode = "P009", NetPrice = 120m, GrossPrice = 147.6m, Quantity = 2, OrderId = 5 },
            new OrderLine { Id = 10, ProductCode = "P010", NetPrice = 40m, GrossPrice = 49.2m, Quantity = 4, OrderId = 5 }
        );
    }
}
