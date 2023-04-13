using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evento.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordTimeExpired",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ResetPasswordUsed",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordTimeExpired",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ResetPasswordUsed",
                table: "User");
        }
    }
}
