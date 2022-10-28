using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WesternInn_MajorProject.Data.Migrations
{
    public partial class view1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ViewStats",
                columns: table => new
                {
                    Month = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<string>(type: "TEXT", nullable: false),
                    Ammount_Booked = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViewStats");
        }
    }
}
