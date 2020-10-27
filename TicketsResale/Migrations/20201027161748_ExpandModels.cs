using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsResale.Migrations
{
    public partial class ExpandModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AspNetUsers_BuyerId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SellerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_BuyerId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TicketsCartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "StoreUserId",
                table: "TicketsCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SellerId",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerId1",
                table: "Tickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SellerId1",
                table: "Tickets",
                column: "SellerId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TicketsCartId",
                table: "AspNetUsers",
                column: "TicketsCartId",
                unique: true);

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
                name: "FK_Tickets_AspNetUsers_SellerId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SellerId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TicketsCartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StoreUserId",
                table: "TicketsCarts");

            migrationBuilder.DropColumn(
                name: "SellerId1",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "SellerId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SellerId",
                table: "Tickets",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_BuyerId",
                table: "CartItems",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TicketsCartId",
                table: "AspNetUsers",
                column: "TicketsCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_BuyerId",
                table: "CartItems",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId",
                table: "Tickets",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
