using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixedRoomId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Rooms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Rooms",
                type: "int",
                nullable: true);
        }
    }
}
