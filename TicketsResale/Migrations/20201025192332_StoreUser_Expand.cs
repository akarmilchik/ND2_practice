using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsResale.Migrations
{
    public partial class StoreUser_Expand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SellerId1",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BuyerId1",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TicketsCartId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SellerId1",
                table: "Tickets",
                column: "SellerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerId1",
                table: "Orders",
                column: "BuyerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers",
                column: "TicketsCartId",
                principalTable: "TicketsCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_BuyerId1",
                table: "Orders",
                column: "BuyerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId1",
                table: "Tickets",
                column: "SellerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_BuyerId1",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SellerId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BuyerId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SellerId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerId1",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "TicketsCartId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers",
                column: "TicketsCartId",
                principalTable: "TicketsCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
