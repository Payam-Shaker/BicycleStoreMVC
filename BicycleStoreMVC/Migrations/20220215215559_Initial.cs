using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BicycleStoreMVC.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Production");

            migrationBuilder.EnsureSchema(
                name: "Sales");

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "Production",
                columns: table => new
                {
                    BrandID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand_Name = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Brand_Id", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Production",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Category_Id", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Sales",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_FirstName = table.Column<string>(type: "varchar(255)", nullable: false),
                    Customer_LastName = table.Column<string>(type: "varchar(255)", nullable: false),
                    Customer_Phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    Customer_Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Customer_Street = table.Column<string>(type: "varchar(255)", nullable: true),
                    Customer_City = table.Column<string>(type: "varchar(255)", nullable: true),
                    Customer_ZipCode = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Customer_Id", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "Sales",
                columns: table => new
                {
                    StoreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Store_Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Store_Phon = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Store_Email = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Store_Street = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Store_City = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Store_ZipCode = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Store_Id", x => x.StoreID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Production",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Product_Year = table.Column<short>(type: "SMALLINT", nullable: false),
                    Product_Price = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    Category_Id = table.Column<int>(type: "INT", nullable: false),
                    Brand_Id = table.Column<int>(type: "INT", nullable: false),
                    Brand_Id1 = table.Column<int>(nullable: true),
                    Category_Id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Product_Id", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_Brand_Brand_Id",
                        column: x => x.Brand_Id,
                        principalSchema: "Production",
                        principalTable: "Brand",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Category_Category_Id",
                        column: x => x.Category_Id,
                        principalSchema: "Production",
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                schema: "Sales",
                columns: table => new
                {
                    StaffID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_FirstName = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Staff_LastName = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Staff_Email = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Staff_Phone = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Store_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Staff_Id", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK_Staff_Store_Store_Id",
                        column: x => x.Store_Id,
                        principalSchema: "Sales",
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                schema: "Production",
                columns: table => new
                {
                    Store_Id = table.Column<int>(type: "INT", nullable: false),
                    Product_Id = table.Column<int>(type: "INT", nullable: false),
                    Stock_Quantity = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Store_Id);
                    table.ForeignKey(
                        name: "FK_Product_Id",
                        column: x => x.Product_Id,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Store_Id",
                        column: x => x.Store_Id,
                        principalSchema: "Sales",
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Sales",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Id = table.Column<int>(type: "INT", nullable: false),
                    Order_Status = table.Column<short>(type: "SMALLINT", nullable: false),
                    Order_Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    Order_Required_Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    Order_Shipped_Date = table.Column<DateTime>(type: "DATE", nullable: true),
                    Store_Id = table.Column<int>(type: "INT", nullable: false),
                    Staff_Id = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Order_Id", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Customer_Id",
                        column: x => x.Customer_Id,
                        principalSchema: "Sales",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_Id",
                        column: x => x.Staff_Id,
                        principalSchema: "Sales",
                        principalTable: "Staff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Store_Id",
                        column: x => x.Store_Id,
                        principalSchema: "Sales",
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order_Item",
                schema: "Sales",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    Product_Id = table.Column<int>(type: "INT", nullable: false),
                    Item_Quantity = table.Column<int>(type: "INT", nullable: false),
                    List_Price = table.Column<decimal>(type: "DECIMAL (10,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "DECIMAL (4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Item", x => new { x.OrderID, x.ItemID });
                    table.ForeignKey(
                        name: "FK_Order_Id",
                        column: x => x.OrderID,
                        principalSchema: "Sales",
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Id",
                        column: x => x.Product_Id,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Brand_Id",
                schema: "Production",
                table: "Product",
                column: "Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Category_Id",
                schema: "Production",
                table: "Product",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Product_Id",
                schema: "Production",
                table: "Stock",
                column: "Product_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Customer_Id",
                schema: "Sales",
                table: "Order",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Staff_Id",
                schema: "Sales",
                table: "Order",
                column: "Staff_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Store_Id",
                schema: "Sales",
                table: "Order",
                column: "Store_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Item_OrderID",
                schema: "Sales",
                table: "Order_Item",
                column: "OrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Item_Product_Id",
                schema: "Sales",
                table: "Order_Item",
                column: "Product_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Store_Id",
                schema: "Sales",
                table: "Staff",
                column: "Store_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stock",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Order_Item",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Staff",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "Sales");
        }
    }
}
