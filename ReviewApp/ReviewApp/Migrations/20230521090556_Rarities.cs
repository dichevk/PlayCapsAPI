using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayCapsViewer.Migrations
{
    /// <inheritdoc />
    public partial class Rarities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rarity",
                table: "PlayCap",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rarity",
                table: "PlayCap");
        }
    }
}
