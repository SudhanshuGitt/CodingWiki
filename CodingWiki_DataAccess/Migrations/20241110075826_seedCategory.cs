using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CATEGORIES VALUES ('CAT 1')");
            migrationBuilder.Sql("INSERT INTO CATEGORIES VALUES ('CAT 2')");
            migrationBuilder.Sql("INSERT INTO CATEGORIES VALUES ('CAT 3')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
