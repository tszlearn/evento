using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evento.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAcronymPropertyToEventModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Acronym",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acronym",
                table: "Event");
        }
    }
}
