using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCompany.Context.Migrations
{
    public partial class modifyAdminUserPasswordInUsersTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "P@ssw0rd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$RGtCyrCUwB9B3.SoaaIEm.4z0g2KdBdNoqZjOVunOKKrbS/1./TmS");
        }
    }
}
