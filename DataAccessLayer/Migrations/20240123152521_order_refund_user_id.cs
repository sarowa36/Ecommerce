using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class order_refund_user_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderRefunds",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRefunds_UserId",
                table: "OrderRefunds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRefunds_AspNetUsers_UserId",
                table: "OrderRefunds",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRefunds_AspNetUsers_UserId",
                table: "OrderRefunds");

            migrationBuilder.DropIndex(
                name: "IX_OrderRefunds_UserId",
                table: "OrderRefunds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderRefunds");
        }
    }
}
