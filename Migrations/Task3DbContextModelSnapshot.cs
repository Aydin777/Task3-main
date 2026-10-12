using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Task3.Data;

#nullable disable

namespace Task3.Migrations;

[DbContext(typeof(Task3DbContext))]
partial class Task3DbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "10.0.10")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("Task3.Entities.Book", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int").UseIdentityColumn();
            b.Property<string>("Author").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
            b.Property<string>("Category").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
            b.Property<bool>("IsAvailable").HasColumnType("bit");
            b.Property<int>("PageCount").HasColumnType("int");
            b.Property<decimal>("Price").HasColumnType("decimal(18,2)");
            b.Property<int>("PublishedYear").HasColumnType("int");
            b.Property<int>("Stock").HasColumnType("int");
            b.Property<string>("Title").IsRequired().HasMaxLength(200).HasColumnType("nvarchar(200)");
            b.HasKey("Id");
            b.HasData(
                new { Id = 1, Author = "Robert C. Martin", Category = "Programming", IsAvailable = true, PageCount = 464, Price = 45m, PublishedYear = 2008, Stock = 10, Title = "Clean Code" },
                new { Id = 2, Author = "James Clear", Category = "Self Development", IsAvailable = true, PageCount = 320, Price = 30m, PublishedYear = 2018, Stock = 8, Title = "Atomic Habits" },
                new { Id = 3, Author = "George Orwell", Category = "Novel", IsAvailable = true, PageCount = 328, Price = 20m, PublishedYear = 1949, Stock = 5, Title = "1984" },
                new { Id = 4, Author = "J.R.R. Tolkien", Category = "Fantasy", IsAvailable = true, PageCount = 310, Price = 35m, PublishedYear = 1937, Stock = 7, Title = "The Hobbit" },
                new { Id = 5, Author = "Robert Kiyosaki", Category = "Finance", IsAvailable = false, PageCount = 336, Price = 25m, PublishedYear = 1997, Stock = 0, Title = "Rich Dad Poor Dad" },
                new { Id = 6, Author = "Andrew Hunt", Category = "Programming", IsAvailable = true, PageCount = 352, Price = 50m, PublishedYear = 1999, Stock = 6, Title = "The Pragmatic Programmer" });
        });
#pragma warning restore 612, 618
    }
}
