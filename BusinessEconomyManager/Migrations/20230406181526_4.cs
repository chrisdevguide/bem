using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessEconomyManager.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("AccountCreditCardCardBalance", "BusinessPeriods", "AccountCreditCardBalance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("AccountCreditCardCardBalance", "BusinessPeriods", "AccountCreditCardBalance");
        }
    }
}
