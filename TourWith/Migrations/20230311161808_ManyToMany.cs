using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourWith.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Destinations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_UserId",
                table: "Destinations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Users_UserId",
                table: "Destinations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Users_UserId",
                table: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_Destinations_UserId",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Destinations");
        }
    }
}
