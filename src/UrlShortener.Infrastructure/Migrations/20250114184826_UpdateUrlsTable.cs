using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUrlsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortenedUrl",
                table: "Urls",
                newName: "ShortUrl");

            migrationBuilder.RenameColumn(
                name: "OriginalUrl",
                table: "Urls",
                newName: "LongUrl");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Urls",
                newName: "CreatedOnUtc");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Urls",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Urls");

            migrationBuilder.RenameColumn(
                name: "ShortUrl",
                table: "Urls",
                newName: "ShortenedUrl");

            migrationBuilder.RenameColumn(
                name: "LongUrl",
                table: "Urls",
                newName: "OriginalUrl");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Urls",
                newName: "CreatedDate");
        }
    }
}
