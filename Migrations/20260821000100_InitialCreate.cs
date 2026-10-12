using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task3.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),

                Title = table.Column<string>(maxLength: 200, nullable: false),
                Author = table.Column<string>(maxLength: 150, nullable: false),
                Category = table.Column<string>(maxLength: 100, nullable: false),

                Price = table.Column<decimal>(nullable: false),
                Stock = table.Column<int>(nullable: false),
                PageCount = table.Column<int>(nullable: false),
                PublishedYear = table.Column<int>(nullable: false),
                IsAvailable = table.Column<bool>(nullable: false)
            },

            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Id);
            }
        );

        migrationBuilder.InsertData(
            "Books",
            new[]
            {
                "Id",
                "Title",
                "Author",
                "Category",
                "Price",
                "Stock",
                "PageCount",
                "PublishedYear",
                "IsAvailable"
            },
            new object[,]
            {
                { 1, "Clean Code", "Robert C. Martin", "Programming", 45m, 10, 464, 2008, true },
                { 2, "Atomic Habits", "James Clear", "Self Development", 30m, 8, 320, 2018, true },
                { 3, "1984", "George Orwell", "Novel", 20m, 5, 328, 1949, true },
                { 4, "The Hobbit", "J.R.R. Tolkien", "Fantasy", 35m, 7, 310, 1937, true },
                { 5, "Rich Dad Poor Dad", "Robert Kiyosaki", "Finance", 25m, 0, 336, 1997, false },
                { 6, "The Pragmatic Programmer", "Andrew Hunt", "Programming", 50m, 6, 352, 1999, true }
            }
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Books");
    }
}