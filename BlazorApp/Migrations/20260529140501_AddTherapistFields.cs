using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTherapistFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttachmentUrl",
                table: "TherapistReviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "TherapistReviews",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEducationVerified",
                table: "TherapistProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPassportVerified",
                table: "TherapistProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelfEmployed",
                table: "TherapistProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SpecialNeeds",
                table: "TherapistProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpeechDisorders",
                table: "TherapistProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkType",
                table: "TherapistProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentUrl",
                table: "TherapistReviews");

            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "TherapistReviews");

            migrationBuilder.DropColumn(
                name: "IsEducationVerified",
                table: "TherapistProfiles");

            migrationBuilder.DropColumn(
                name: "IsPassportVerified",
                table: "TherapistProfiles");

            migrationBuilder.DropColumn(
                name: "IsSelfEmployed",
                table: "TherapistProfiles");

            migrationBuilder.DropColumn(
                name: "SpecialNeeds",
                table: "TherapistProfiles");

            migrationBuilder.DropColumn(
                name: "SpeechDisorders",
                table: "TherapistProfiles");

            migrationBuilder.DropColumn(
                name: "WorkType",
                table: "TherapistProfiles");
        }
    }
}
