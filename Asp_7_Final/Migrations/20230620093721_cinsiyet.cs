using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asp_7_Final.Migrations
{
    /// <inheritdoc />
    public partial class cinsiyet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cinsiyet",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cinsiyet",
                table: "Users");
        }
    }
}
