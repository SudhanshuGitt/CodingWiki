﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddViewAndSproc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW dbo.GetMainBookDetails
                AS
                SELECT  m.ISBM,m.Title,m.Price FROM dbo.Books m
            ");

            migrationBuilder.Sql(@"CREATE OR ALTER VIEW dbo.GetAllBookDetails
                AS
                SELECT * FROM dbo.Books m
            ");


            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.getAllBookDetailById   
                    @bookId int
                AS   
                    SET NOCOUNT ON;  
                    SELECT  *  FROM dbo.Books b
	                WHERE b.BookId=@bookId
                GO  
            ");

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.GetMainBookDetails");
            migrationBuilder.Sql("DROP VIEW dbo.GetAllBookDetails");
            migrationBuilder.Sql("DROP PROCEDURE dbo.getAllBookDetailById");
        }
    }
}
