using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBook_Seller.Migrations
{
    /// <inheritdoc />
    public partial class AddedSellerBookModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellerBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerBooks", x => x.Id);
                    table.CheckConstraint("CK_SellerBook_Discount", "[Discount]>=0 AND [Discount]<=100");
                    table.CheckConstraint("CK_SellerBook_Price", "[Price]>0");
                    table.ForeignKey(
                        name: "FK_SellerBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellerBooks_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellerBooks_BookId",
                table: "SellerBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerBooks_SellerId",
                table: "SellerBooks",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellerBooks");
        }
    }
}
