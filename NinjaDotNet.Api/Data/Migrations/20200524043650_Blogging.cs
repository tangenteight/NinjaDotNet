using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaDotNet.Api.Data.Migrations
{
    public partial class Blogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogDetails",
                columns: table => new
                {
                    BlogDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<Guid>(nullable: false),
                    Synopsis = table.Column<string>(nullable: true),
                    BodyHtml = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogDetails", x => x.BlogDetailId);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Author = table.Column<Guid>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    BlogDetailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blog_BlogDetails_BlogDetailId",
                        column: x => x.BlogDetailId,
                        principalTable: "BlogDetails",
                        principalColumn: "BlogDetailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogComment",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<Guid>(nullable: false),
                    Commenter = table.Column<Guid>(nullable: false),
                    CommentText = table.Column<string>(nullable: true),
                    BlogId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_BlogComment_Blog_BlogId1",
                        column: x => x.BlogId1,
                        principalTable: "Blog",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogDetailId",
                table: "Blog",
                column: "BlogDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComment_BlogId1",
                table: "BlogComment",
                column: "BlogId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogComment");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "BlogDetails");
        }
    }
}
