using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StandApp.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sets");
        }
    }
}
