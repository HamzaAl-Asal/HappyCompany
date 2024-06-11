using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCompany.Context.Migrations
{
    public partial class modifyPasswordHashNameInUsersTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");
        }
    }
}
