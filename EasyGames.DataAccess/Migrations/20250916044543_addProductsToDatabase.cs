using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyGames.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addProductsToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Harry Potter is the \"Boy Who Lived,\" a young wizard with distinctive green eyes, messy black hair, and a lightning bolt-shaped scar on his forehead, who discovers his true identity after surviving an attack by the dark wizard Lord Voldemort as a baby", "Harry Potter", 0.01m },
                    { 2, "The Witcher 3: Wild Hunt is an open-world action RPG where players control Geralt of Rivia, a monster hunter searching for his adoptive daughter, Ciri, who is being pursued by the otherworldly Wild Hunt.", "The Witcher 3", 0.02m },
                    { 3, "Make Cool Tricks YOO", "Yoyo", 0.03m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
