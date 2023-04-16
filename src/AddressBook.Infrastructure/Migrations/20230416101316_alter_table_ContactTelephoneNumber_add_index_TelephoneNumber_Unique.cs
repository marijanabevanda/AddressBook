using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBook.Infrastructure.Migrations
{
    public partial class alter_table_ContactTelephoneNumber_add_index_TelephoneNumber_Unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContactTelephoneNumber_TelephoneNumber",
                table: "ContactTelephoneNumber",
                column: "TelephoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactTelephoneNumber_TelephoneNumber",
                table: "ContactTelephoneNumber");
        }
    }
}
