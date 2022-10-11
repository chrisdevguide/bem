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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserBusinesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBusinesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBusinesses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceSuppliedTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserBusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSuppliedTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceSuppliedTypes_UserBusinesses_UserBusinessId",
                        column: x => x.UserBusinessId,
                        principalTable: "UserBusinesses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserBusinessPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserBusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBusinessPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBusinessPeriods_UserBusinesses_UserBusinessId",
                        column: x => x.UserBusinessId,
                        principalTable: "UserBusinesses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserBusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceSuppliedTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_ServiceSuppliedTypes_ServiceSuppliedTypeId",
                        column: x => x.ServiceSuppliedTypeId,
                        principalTable: "ServiceSuppliedTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suppliers_UserBusinesses_UserBusinessId",
                        column: x => x.UserBusinessId,
                        principalTable: "UserBusinesses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidAmount = table.Column<double>(type: "float", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    UserBusinessPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierOperations_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierOperations_UserBusinessPeriods_UserBusinessPeriodId",
                        column: x => x.UserBusinessPeriodId,
                        principalTable: "UserBusinessPeriods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSuppliedTypes_UserBusinessId",
                table: "ServiceSuppliedTypes",
                column: "UserBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOperations_SupplierId",
                table: "SupplierOperations",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOperations_UserBusinessPeriodId",
                table: "SupplierOperations",
                column: "UserBusinessPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ServiceSuppliedTypeId",
                table: "Suppliers",
                column: "ServiceSuppliedTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UserBusinessId",
                table: "Suppliers",
                column: "UserBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBusinesses_UserId",
                table: "UserBusinesses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBusinessPeriods_UserBusinessId",
                table: "UserBusinessPeriods",
                column: "UserBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailAddress",
                table: "Users",
                column: "EmailAddress",
                unique: true,
                filter: "[EmailAddress] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierOperations");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "UserBusinessPeriods");

            migrationBuilder.DropTable(
                name: "ServiceSuppliedTypes");

            migrationBuilder.DropTable(
                name: "UserBusinesses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
