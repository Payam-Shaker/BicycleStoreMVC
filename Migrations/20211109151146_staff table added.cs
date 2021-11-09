using Microsoft.EntityFrameworkCore.Migrations;

namespace BicycleStoreMVC.Migrations
{
    public partial class stafftableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffID",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    StoreID = table.Column<int>(nullable: true),
                    ManagerID = table.Column<int>(nullable: true),
                    StaffID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK_Staffs_Staffs_StaffID1",
                        column: x => x.StaffID1,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staffs_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StaffID",
                table: "Orders",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_StaffID1",
                table: "Staffs",
                column: "StaffID1");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_StoreID",
                table: "Staffs",
                column: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Staffs_StaffID",
                table: "Orders",
                column: "StaffID",
                principalTable: "Staffs",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Staffs_StaffID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StaffID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StaffID",
                table: "Orders");
        }
    }
}
