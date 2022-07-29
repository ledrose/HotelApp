using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.Migrations
{
    public partial class updated_room_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "Rooms",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "priceWorkday",
                table: "Rooms",
                newName: "PriceWorkday");

            migrationBuilder.RenameColumn(
                name: "priceWeekends",
                table: "Rooms",
                newName: "PriceWeekends");

            migrationBuilder.AddColumn<int>(
                name: "SpotNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotNumber",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Rooms",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "PriceWorkday",
                table: "Rooms",
                newName: "priceWorkday");

            migrationBuilder.RenameColumn(
                name: "PriceWeekends",
                table: "Rooms",
                newName: "priceWeekends");
        }
    }
}
