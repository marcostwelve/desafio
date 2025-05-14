using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDebtTitleInstallments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "DebtTitles");

            migrationBuilder.DropColumn(
                name: "InstallmentNumber",
                table: "DebtTitles");

            migrationBuilder.DropColumn(
                name: "InstallmentValue",
                table: "DebtTitles");

            migrationBuilder.DropColumn(
                name: "Installments",
                table: "DebtTitles");

            migrationBuilder.CreateTable(
                name: "DebtInstallment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallmentNumber = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallmentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebtTitleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtInstallment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DebtInstallment_DebtTitles_DebtTitleId",
                        column: x => x.DebtTitleId,
                        principalTable: "DebtTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DebtInstallment_DebtTitleId",
                table: "DebtInstallment",
                column: "DebtTitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DebtInstallment");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "DebtTitles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InstallmentNumber",
                table: "DebtTitles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "InstallmentValue",
                table: "DebtTitles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Installments",
                table: "DebtTitles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
