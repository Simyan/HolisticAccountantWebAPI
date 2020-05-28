using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HolisticAccountant.Migrations
{
    public partial class AddTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    UId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Merchant = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    PurchasedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.UId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
