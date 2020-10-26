using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsResale.Migrations
{
    public partial class ExpandStoreUserWithIdentityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Orders");

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
                table: "CartItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BuyerId1",
                table: "CartItems",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "CartItems",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "TrackNumber",
                table: "CartItems",
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
                name: "IX_CartItems_BuyerId1",
                table: "CartItems",
                column: "BuyerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TicketsCarts_TicketsCartId",
                table: "AspNetUsers",
                column: "TicketsCartId",
                principalTable: "TicketsCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_BuyerId1",
                table: "CartItems",
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
                name: "FK_CartItems_AspNetUsers_BuyerId1",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SellerId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_BuyerId1",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SellerId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "BuyerId1",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "TrackNumber",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "TicketsCartId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    TrackNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TicketId",
                table: "Orders",
                column: "TicketId");

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
