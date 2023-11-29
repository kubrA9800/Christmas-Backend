using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristmasBackend.Migrations
{
    public partial class UpdatesAdvertTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Head",
                table: "Adverts");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Adverts",
                newName: "Text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Adverts",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Head",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
