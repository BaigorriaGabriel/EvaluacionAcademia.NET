using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluacionAcademia.NET.Migrations
{
    public partial class CryptoWallet17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    codTransaction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codAccountSender = table.Column<int>(type: "Int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fromCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    toCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codAccountReceiver = table.Column<int>(type: "Int", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.codTransaction);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_codAccountSender",
                        column: x => x.codAccountSender,
                        principalTable: "Accounts",
                        principalColumn: "codAccount",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_codAccountSender",
                table: "Transactions",
                column: "codAccountSender");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
