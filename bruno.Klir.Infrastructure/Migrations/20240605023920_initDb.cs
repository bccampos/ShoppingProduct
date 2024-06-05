using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bruno.Klir.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyAtEach = table.Column<int>(type: "int", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsPromotionActive = table.Column<bool>(type: "bit", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shopping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shopping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromotionApplied = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingItems_Shopping_ShoppingGroupId",
                        column: x => x.ShoppingGroupId,
                        principalTable: "Shopping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "IsActive", "Name", "Price", "PromotionId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("431f430f-bb42-462f-9294-68488f0e31a4"), true, "Product D", 4.00m, null, 0 },
                    { new Guid("b5b566ad-1406-4a58-a3a6-76318c60e0af"), true, "Product C", 2.00m, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "ApplyAtEach", "DiscountType", "DiscountValue", "IsPromotionActive", "Name", "Quantity" },
                values: new object[,]
                {
                    { new Guid("47133cb6-1135-4d05-b44a-bdfe41eb1b45"), 0, 2, 0m, true, "3 for 10 Euro", 2 },
                    { new Guid("ba35b678-9289-45ce-a601-80f59cec908e"), 0, 1, 0m, true, "Buy 1 Get 1 Free", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "IsActive", "Name", "Price", "PromotionId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("61420e17-37fb-4c34-9a5c-7bc45a75aa3e"), true, "Product A", 20.00m, new Guid("ba35b678-9289-45ce-a601-80f59cec908e"), 0 },
                    { new Guid("c81102fd-f418-4497-bbbc-1d8bf677f9e8"), true, "Product B", 4.00m, new Guid("47133cb6-1135-4d05-b44a-bdfe41eb1b45"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PromotionId",
                table: "Products",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_ProductId",
                table: "ShoppingItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_ShoppingGroupId",
                table: "ShoppingItems",
                column: "ShoppingGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shopping");

            migrationBuilder.DropTable(
                name: "Promotions");
        }
    }
}
