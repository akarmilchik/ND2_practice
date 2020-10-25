using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsResale.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "TicketsCartId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers",
                column: "TicketsCartId",
                principalTable: "TicketsCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "TicketsCartId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers",
                column: "TicketsCartId",
                principalTable: "TicketsCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
