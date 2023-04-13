using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evento.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Event_EventId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_UserId",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Ticket",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Ticket",
                newName: "EventID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ticket",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                newName: "IX_Ticket_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_EventId",
                table: "Ticket",
                newName: "IX_Ticket_EventID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Event",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Event_EventID",
                table: "Ticket",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_UserID",
                table: "Ticket",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Event_EventID",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_UserID",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Ticket",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "Ticket",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Ticket",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_UserID",
                table: "Ticket",
                newName: "IX_Ticket_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_EventID",
                table: "Ticket",
                newName: "IX_Ticket_EventId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Event",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Event_EventId",
                table: "Ticket",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_UserId",
                table: "Ticket",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
