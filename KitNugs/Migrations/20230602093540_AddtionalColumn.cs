using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitNugs.Migrations
{
    /// <inheritdoc />
    public partial class AddtionalColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnotherColumn",
                table: "HelloTable",
                type: "varchar(50)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnotherColumn",
                table: "HelloTable");
        }
    }
}
