using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace baitapapinetcore.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderDetail_OrderDetailId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderDetailId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_DonHangID",
                table: "OrderDetail",
                column: "DonHangID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Order_DonHangID",
                table: "OrderDetail",
                column: "DonHangID",
                principalTable: "Order",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Order_DonHangID",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_DonHangID",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderDetailId",
                table: "Order",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderDetail_OrderDetailId",
                table: "Order",
                column: "OrderDetailId",
                principalTable: "OrderDetail",
                principalColumn: "Id");
        }
    }
}
