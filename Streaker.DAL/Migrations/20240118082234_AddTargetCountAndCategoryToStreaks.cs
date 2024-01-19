using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streaker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetCountAndCategoryToStreaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Streaks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TargetCount",
                table: "Streaks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Streaks");

            migrationBuilder.DropColumn(
                name: "TargetCount",
                table: "Streaks");
        }
    }
}
