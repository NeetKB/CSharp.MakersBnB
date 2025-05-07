using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakersBnB.Migrations
{
    /// <inheritdoc />
    public partial class AddedRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "rules",
                table: "Spaces",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rules",
                table: "Spaces");
        }
    }
}
