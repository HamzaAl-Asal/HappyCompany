using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCompany.Context.Migrations
{
    public partial class addUniqueConstraintToWarehouseAndItem_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_Name",
                table: "Warehouses",
                column: "Name",
                unique: true);  

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Warehouses_Name",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Items_Name",
                table: "Items");
        }
    }
}
