using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class AddReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "TherapistProfiles");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistReviews_TherapistProfileId",
                table: "TherapistReviews",
                column: "TherapistProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistReviews_TherapistProfiles_TherapistProfileId",
                table: "TherapistReviews",
                column: "TherapistProfileId",
                principalTable: "TherapistProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TherapistReviews_TherapistProfiles_TherapistProfileId",
                table: "TherapistReviews");

            migrationBuilder.DropIndex(
                name: "IX_TherapistReviews_TherapistProfileId",
                table: "TherapistReviews");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "TherapistProfiles",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
