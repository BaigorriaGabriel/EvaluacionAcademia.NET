using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluacionAcademia.NET.Migrations
{
    public partial class CryptoWallet21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "balanceUsd",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "balancePeso",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "balanceBtc",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "codAccount",
                keyValue: 2,
                column: "balanceBtc",
                value: 5m);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "codAccount",
                keyValue: 1,
                columns: new[] { "balancePeso", "balanceUsd" },
                values: new object[] { 250m, 10m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "balanceUsd",
                table: "Accounts",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "balancePeso",
                table: "Accounts",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "balanceBtc",
                table: "Accounts",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "codAccount",
                keyValue: 2,
                column: "balanceBtc",
                value: 5f);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "codAccount",
                keyValue: 1,
                columns: new[] { "balancePeso", "balanceUsd" },
                values: new object[] { 250f, 10f });
        }
    }
}
