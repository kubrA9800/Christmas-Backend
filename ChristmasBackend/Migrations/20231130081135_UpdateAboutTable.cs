using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristmasBackend.Migrations
{
    public partial class UpdateAboutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Abouts",
                newName: "Video");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Abouts",
                newName: "Content");
        }
    }
}
