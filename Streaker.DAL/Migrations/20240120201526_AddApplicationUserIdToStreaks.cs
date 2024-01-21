using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streaker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserIdToStreaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Streaks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Streaks_ApplicationUserId",
                table: "Streaks",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Streaks_AspNetUsers_ApplicationUserId",
                table: "Streaks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Streaks_AspNetUsers_ApplicationUserId",
                table: "Streaks");

            migrationBuilder.DropIndex(
                name: "IX_Streaks_ApplicationUserId",
                table: "Streaks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Streaks");
        }
    }
}
