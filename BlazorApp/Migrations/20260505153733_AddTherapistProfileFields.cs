using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTherapistProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullBio",
                table: "TherapistProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TherapistProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "TherapistProfiles",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "TherapistProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkFormat",
                table: "TherapistProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullBio",
                table: "TherapistProfiles");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TherapistProfiles");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "TherapistProfiles");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "TherapistProfiles");

            migrationBuilder.DropColumn(
                name: "WorkFormat",
                table: "TherapistProfiles");
        }
    }
}
