using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessEconomyManager.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateFrom = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateTo = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AccountCashBalance = table.Column<double>(type: "double precision", nullable: false),
                    AccountCreditCardBalance = table.Column<double>(type: "double precision", nullable: false),
                    Closed = table.Column<bool>(type: "boolean", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessPeriods_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierCategories_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessSaleTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TransactionPaymentType = table.Column<int>(type: "integer", nullable: false),
                    BusinessPeriodId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSaleTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessSaleTransactions_BusinessPeriods_BusinessPeriodId",
                        column: x => x.BusinessPeriodId,
                        principalTable: "BusinessPeriods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suppliers_SupplierCategories_SupplierCategoryId",
                        column: x => x.SupplierCategoryId,
                        principalTable: "SupplierCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessExpenseTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    TransactionPaymentType = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessPeriodId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessExpenseTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessExpenseTransactions_BusinessPeriods_BusinessPeriodId",
                        column: x => x.BusinessPeriodId,
                        principalTable: "BusinessPeriods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessExpenseTransactions_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_EmailAddress",
                table: "AppUsers",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_AppUserId",
                table: "Businesses",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessExpenseTransactions_BusinessPeriodId",
                table: "BusinessExpenseTransactions",
                column: "BusinessPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessExpenseTransactions_SupplierId",
                table: "BusinessExpenseTransactions",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessPeriods_BusinessId",
                table: "BusinessPeriods",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSaleTransactions_BusinessPeriodId",
                table: "BusinessSaleTransactions",
                column: "BusinessPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierCategories_BusinessId",
                table: "SupplierCategories",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_BusinessId",
                table: "Suppliers",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierCategoryId",
                table: "Suppliers",
                column: "SupplierCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessExpenseTransactions");

            migrationBuilder.DropTable(
                name: "BusinessSaleTransactions");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "BusinessPeriods");

            migrationBuilder.DropTable(
                name: "SupplierCategories");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
