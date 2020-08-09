using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerCode = table.Column<string>(nullable: true),
                    DealerName = table.Column<string>(nullable: true),
                    DealerType = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<int>(nullable: false),
                    TaxNumber = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 10, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    IdentityNumber = table.Column<string>(maxLength: 11, nullable: false),
                    WebSiteURL = table.Column<string>(nullable: true),
                    CallBackAddress = table.Column<string>(nullable: true),
                    NaceCode = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gateways",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Gateways_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Limits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    IsThreeDRequired = table.Column<bool>(nullable: false),
                    DailyTrxAmountLimit = table.Column<decimal>(nullable: false),
                    DailyTrxNumberLimit = table.Column<int>(nullable: false),
                    DailyTrxAmountLimitNon3D = table.Column<decimal>(nullable: false),
                    DailyTrxNumberLimitNon3D = table.Column<int>(nullable: false),
                    EachTrxAmountLimit = table.Column<decimal>(nullable: false),
                    EachTrxAmountLimitNon3D = table.Column<decimal>(nullable: false),
                    DailyCardAmountLimit = table.Column<decimal>(nullable: false),
                    DailyCardNumberLimit = table.Column<int>(nullable: false),
                    DailyCardNumberAlertLimit = table.Column<int>(nullable: false),
                    MonthlyTrxAmountLimit = table.Column<decimal>(nullable: false),
                    MonthlyTrxNumberLimit = table.Column<int>(nullable: false),
                    MonthlyTrxAmountLimitNon3D = table.Column<decimal>(nullable: false),
                    MonthlyTrxNumberLimitNon3D = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limits", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Limits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatewayId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Merchant_Gateways_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "Gateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MerchantUri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatewayId = table.Column<int>(nullable: false),
                    GatewayUri = table.Column<string>(nullable: true),
                    GateUri = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantUri_Gateways_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "Gateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatewayId = table.Column<int>(nullable: false),
                    Installment = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Gateways_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "Gateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<string>(nullable: true),
                    RatesId = table.Column<int>(nullable: true),
                    RateId = table.Column<int>(nullable: false),
                    RequestMode = table.Column<int>(nullable: false),
                    SecurityLevel = table.Column<int>(nullable: false),
                    ProcessType = table.Column<int>(nullable: false),
                    ConfirmationCode = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Debt = table.Column<decimal>(nullable: false),
                    Commission = table.Column<decimal>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Transactions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Rates_RatesId",
                        column: x => x.RatesId,
                        principalTable: "Rates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(nullable: false),
                    Raw = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gateways_ClientId",
                table: "Gateways",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Limits_ClientId",
                table: "Limits",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_TransactionId",
                table: "Log",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_GatewayId",
                table: "Merchant",
                column: "GatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantUri_GatewayId",
                table: "MerchantUri",
                column: "GatewayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_GatewayId",
                table: "Rates",
                column: "GatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ClientId",
                table: "Role",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ParentId",
                table: "Transactions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RatesId",
                table: "Transactions",
                column: "RatesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Limits");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.DropTable(
                name: "MerchantUri");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Gateways");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
