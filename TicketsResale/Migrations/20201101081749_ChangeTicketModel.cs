using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsResale.Migrations
{
    public partial class ChangeTicketModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SellerId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SellerId1",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "SellerId",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SellerId",
                table: "Tickets",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId",
                table: "Tickets",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SellerId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "SellerId",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerId1",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SellerId1",
                table: "Tickets",
                column: "SellerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId1",
                table: "Tickets",
                column: "SellerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
