using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristmasBackend.Migrations
{
    public partial class UpdateContactMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "ContactMessages",
                newName: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "ContactMessages",
                newName: "Text");
        }
    }
}
