using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayCapsViewer.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayCapsCategories_PlayCap_CategoryId",
                table: "PlayCapsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayCapsPlayers_PlayCap_PlayerId",
                table: "PlayCapsPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_PlayCap_PlayCapId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Reviewer_ReviewerId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviewer_Countries_CountryId",
                table: "Reviewer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviewer",
                table: "Reviewer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayCap",
                table: "PlayCap");

            migrationBuilder.RenameTable(
                name: "Reviewer",
                newName: "Reviewers");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "PlayCap",
                newName: "PlayCaps");

            migrationBuilder.RenameIndex(
                name: "IX_Reviewer_CountryId",
                table: "Reviewers",
                newName: "IX_Reviewers_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_ReviewerId",
                table: "Reviews",
                newName: "IX_Reviews_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_PlayCapId",
                table: "Reviews",
                newName: "IX_Reviews_PlayCapId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Reviews",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviewers",
                table: "Reviewers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayCaps",
                table: "PlayCaps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayCapsCategories_PlayCaps_CategoryId",
                table: "PlayCapsCategories",
                column: "CategoryId",
                principalTable: "PlayCaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayCapsPlayers_PlayCaps_PlayerId",
                table: "PlayCapsPlayers",
                column: "PlayerId",
                principalTable: "PlayCaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewers_Countries_CountryId",
                table: "Reviewers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_PlayCaps_PlayCapId",
                table: "Reviews",
                column: "PlayCapId",
                principalTable: "PlayCaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reviewers_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayCapsCategories_PlayCaps_CategoryId",
                table: "PlayCapsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayCapsPlayers_PlayCaps_PlayerId",
                table: "PlayCapsPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviewers_Countries_CountryId",
                table: "Reviewers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_PlayCaps_PlayCapId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reviewers_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviewers",
                table: "Reviewers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayCaps",
                table: "PlayCaps");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Reviewers",
                newName: "Reviewer");

            migrationBuilder.RenameTable(
                name: "PlayCaps",
                newName: "PlayCap");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Review",
                newName: "IX_Review_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PlayCapId",
                table: "Review",
                newName: "IX_Review_PlayCapId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviewers_CountryId",
                table: "Reviewer",
                newName: "IX_Reviewer_CountryId");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Review",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviewer",
                table: "Reviewer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayCap",
                table: "PlayCap",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayCapsCategories_PlayCap_CategoryId",
                table: "PlayCapsCategories",
                column: "CategoryId",
                principalTable: "PlayCap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayCapsPlayers_PlayCap_PlayerId",
                table: "PlayCapsPlayers",
                column: "PlayerId",
                principalTable: "PlayCap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_PlayCap_PlayCapId",
                table: "Review",
                column: "PlayCapId",
                principalTable: "PlayCap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Reviewer_ReviewerId",
                table: "Review",
                column: "ReviewerId",
                principalTable: "Reviewer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewer_Countries_CountryId",
                table: "Reviewer",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
