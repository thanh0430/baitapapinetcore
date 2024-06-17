using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace baitapapinetcore.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VoucherForAcc_voucherId",
                table: "VoucherForAcc");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherForAcc_voucherId",
                table: "VoucherForAcc",
                column: "voucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_IdCreator",
                table: "Voucher",
                column: "IdCreator");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Account_IdCreator",
                table: "Voucher",
                column: "IdCreator",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Account_IdCreator",
                table: "Voucher");

            migrationBuilder.DropIndex(
                name: "IX_VoucherForAcc_voucherId",
                table: "VoucherForAcc");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_IdCreator",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherForAcc_voucherId",
                table: "VoucherForAcc",
                column: "voucherId",
                unique: true);
        }
    }
}
