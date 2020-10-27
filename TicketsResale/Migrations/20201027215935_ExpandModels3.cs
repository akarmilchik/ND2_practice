using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsResale.Migrations
{
    public partial class ExpandModels3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TicketsCartId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BuyerId",
                table: "Tickets",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TicketsCartId",
                table: "AspNetUsers",
                column: "TicketsCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_BuyerId",
                table: "Tickets",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_BuyerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_BuyerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TicketsCartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Tickets");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TicketsCartId",
                table: "AspNetUsers",
                column: "TicketsCartId",
                unique: true);
        }
    }
}
