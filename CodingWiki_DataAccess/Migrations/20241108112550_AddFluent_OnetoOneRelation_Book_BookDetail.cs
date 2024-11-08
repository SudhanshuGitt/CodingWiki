using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFluent_OnetoOneRelation_Book_BookDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Book_Id",
                table: "Fluent_BookDetials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_BookDetials_Book_Id",
                table: "Fluent_BookDetials",
                column: "Book_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookDetials_Fluent_Books_Book_Id",
                table: "Fluent_BookDetials",
                column: "Book_Id",
                principalTable: "Fluent_Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookDetials_Fluent_Books_Book_Id",
                table: "Fluent_BookDetials");

            migrationBuilder.DropIndex(
                name: "IX_Fluent_BookDetials_Book_Id",
                table: "Fluent_BookDetials");

            migrationBuilder.DropColumn(
                name: "Book_Id",
                table: "Fluent_BookDetials");
        }
    }
}
