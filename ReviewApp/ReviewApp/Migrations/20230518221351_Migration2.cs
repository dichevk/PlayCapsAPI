using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayCapsViewer.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Reviewer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviewer_CountryId",
                table: "Reviewer",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewer_Countries_CountryId",
                table: "Reviewer",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviewer_Countries_CountryId",
                table: "Reviewer");

            migrationBuilder.DropIndex(
                name: "IX_Reviewer_CountryId",
                table: "Reviewer");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Reviewer");
        }
    }
}
