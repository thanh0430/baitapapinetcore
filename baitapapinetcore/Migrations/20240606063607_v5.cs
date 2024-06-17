using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace baitapapinetcore.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangID = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false),
                    TenSP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaGoc = table.Column<double>(type: "float", nullable: false),
                    GiaBan = table.Column<double>(type: "float", nullable: false),
                    GiamGia = table.Column<float>(type: "real", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderDetail_OrderDetailId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderDetailId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "Order");
        }
    }
}
