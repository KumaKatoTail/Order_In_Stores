using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order_In_Stores.Migrations
{
    public partial class FixCustomerAddressRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreNumber = table.Column<int>(type: "int", nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    CustomerAddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_CustomerAddressId",
                        column: x => x.CustomerAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "Warszawa", "00-001", "Ulica Kwiatowa 1" },
                    { 2, "Kraków", "30-001", "Ulica Wiosenna 5" },
                    { 3, "Gdańsk", "80-001", "Ulica Zimowa 3" },
                    { 4, "Wrocław", "50-001", "Ulica Letnia 8" },
                    { 5, "Poznań", "60-001", "Ulica Jesienna 12" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerAddressId", "PaymentType", "StoreName", "StoreNumber" },
                values: new object[,]
                {
                    { 1, 1, 1, "Sklep 2", 2 },
                    { 2, 2, 0, "Sklep 4", 4 },
                    { 3, 3, 2, "Sklep 6", 6 },
                    { 4, 4, 1, "Sklep 8", 8 },
                    { 5, 5, 0, "Sklep 10", 10 }
                });

            migrationBuilder.InsertData(
                table: "OrderLines",
                columns: new[] { "Id", "GrossPrice", "NetPrice", "OrderId", "ProductCode", "Quantity" },
                values: new object[,]
                {
                    { 1, 123m, 100m, 1, "P001", 2 },
                    { 2, 61.5m, 50m, 1, "P002", 3 },
                    { 3, 246m, 200m, 2, "P003", 1 },
                    { 4, 98.4m, 80m, 2, "P004", 2 },
                    { 5, 369m, 300m, 3, "P005", 1 },
                    { 6, 73.8m, 60m, 3, "P006", 5 },
                    { 7, 184.5m, 150m, 4, "P007", 2 },
                    { 8, 110.7m, 90m, 4, "P008", 3 },
                    { 9, 147.6m, 120m, 5, "P009", 2 },
                    { 10, 49.2m, 40m, 5, "P010", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerAddressId",
                table: "Orders",
                column: "CustomerAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
