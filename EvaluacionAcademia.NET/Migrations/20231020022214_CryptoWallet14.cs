using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluacionAcademia.NET.Migrations
{
    public partial class CryptoWallet14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    codUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    dni = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    password = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.codUser);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    codAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codUser = table.Column<int>(type: "Int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    directionUUID = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    balanceBtc = table.Column<float>(type: "real", nullable: true),
                    CBU = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    alias = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    accountNumber = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    balancePeso = table.Column<float>(type: "real", nullable: true),
                    balanceUsd = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.codAccount);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_codUser",
                        column: x => x.codUser,
                        principalTable: "Users",
                        principalColumn: "codUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "codUser", "dni", "email", "isActive", "name", "password" },
                values: new object[] { 1, "44504788", "gabi.2912@hotmail.com", true, "Gabriel Baigorria", "1234" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "codAccount", "balanceBtc", "codUser", "directionUUID", "isActive", "type" },
                values: new object[] { 2, 5f, 1, "asd123", true, "Cripto" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "codAccount", "accountNumber", "alias", "balancePeso", "balanceUsd", "CBU", "codUser", "isActive", "type" },
                values: new object[] { 1, "123", "gabriel.baigorria.cw", 250f, 10f, "111111", 1, true, "Fiduciary" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_codUser",
                table: "Accounts",
                column: "codUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
