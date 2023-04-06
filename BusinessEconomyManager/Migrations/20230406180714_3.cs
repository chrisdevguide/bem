using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessEconomyManager.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AccountCashBalance",
                table: "BusinessPeriods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AccountCreditCardBalance",
                table: "BusinessPeriods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCashBalance",
                table: "BusinessPeriods");

            migrationBuilder.DropColumn(
                name: "AccountCreditCardBalance",
                table: "BusinessPeriods");
        }
    }
}
