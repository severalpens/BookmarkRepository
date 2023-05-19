using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookmarkRepository.Migrations
{
    /// <inheritdoc />
    public partial class m121212 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                schema: "bl",
                table: "bookmarks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                schema: "bl",
                table: "bookmarks");
        }
    }
}
