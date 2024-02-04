using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class order_refund_message_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "OrderRefunds",
                newName: "UserMessage");

            migrationBuilder.AddColumn<string>(
                name: "SellerResponse",
                table: "OrderRefunds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerResponse",
                table: "OrderRefunds");

            migrationBuilder.RenameColumn(
                name: "UserMessage",
                table: "OrderRefunds",
                newName: "Message");
        }
    }
}
