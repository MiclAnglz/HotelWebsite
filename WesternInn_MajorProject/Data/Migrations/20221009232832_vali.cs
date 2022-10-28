using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WesternInn_MajorProject.Data.Migrations
{
    public partial class vali : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ammount_Booked",
                table: "ViewStats",
                newName: "Amount_Booked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount_Booked",
                table: "ViewStats",
                newName: "Ammount_Booked");
        }
    }
}
