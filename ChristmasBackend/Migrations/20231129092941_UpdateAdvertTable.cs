using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristmasBackend.Migrations
{
    public partial class UpdateAdvertTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Adverts");
        }
    }
}
