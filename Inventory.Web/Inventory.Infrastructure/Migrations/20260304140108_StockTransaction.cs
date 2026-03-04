using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StockTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Products_ProductId",
                table: "StockTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTransaction",
                table: "StockTransaction");

            migrationBuilder.RenameTable(
                name: "StockTransaction",
                newName: "StockTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransaction_ProductId",
                table: "StockTransactions",
                newName: "IX_StockTransactions_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionType",
                table: "StockTransactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTransactions",
                table: "StockTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Products_ProductId",
                table: "StockTransactions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Products_ProductId",
                table: "StockTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTransactions",
                table: "StockTransactions");

            migrationBuilder.RenameTable(
                name: "StockTransactions",
                newName: "StockTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransactions_ProductId",
                table: "StockTransaction",
                newName: "IX_StockTransaction_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "StockTransaction",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTransaction",
                table: "StockTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Products_ProductId",
                table: "StockTransaction",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
