using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Code.Migrations
{
    public partial class IdeaPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdeaPostId",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdeaPostId",
                table: "Photos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdeaPostId",
                table: "Likes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdeaPostId",
                table: "Idea",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdeaPostId",
                table: "Comment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "IdeaPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Eco = table.Column<string>(nullable: true),
                    EcoUn = table.Column<string>(nullable: true),
                    EcoUnOther = table.Column<string>(nullable: true),
                    Ico = table.Column<string>(nullable: true),
                    IcoOther = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeaPosts_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdeaPosts_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdeaPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_IdeaPostId",
                table: "Project",
                column: "IdeaPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_IdeaPostId",
                table: "Photos",
                column: "IdeaPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_IdeaPostId",
                table: "Likes",
                column: "IdeaPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Idea_IdeaPostId",
                table: "Idea",
                column: "IdeaPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IdeaPostId",
                table: "Comment",
                column: "IdeaPostId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaPosts_CountryId",
                table: "IdeaPosts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaPosts_StateId",
                table: "IdeaPosts",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaPosts_UserId",
                table: "IdeaPosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_IdeaPosts_IdeaPostId",
                table: "Comment",
                column: "IdeaPostId",
                principalTable: "IdeaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Idea_IdeaPosts_IdeaPostId",
                table: "Idea",
                column: "IdeaPostId",
                principalTable: "IdeaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_IdeaPosts_IdeaPostId",
                table: "Likes",
                column: "IdeaPostId",
                principalTable: "IdeaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_IdeaPosts_IdeaPostId",
                table: "Photos",
                column: "IdeaPostId",
                principalTable: "IdeaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_IdeaPosts_IdeaPostId",
                table: "Project",
                column: "IdeaPostId",
                principalTable: "IdeaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_IdeaPosts_IdeaPostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Idea_IdeaPosts_IdeaPostId",
                table: "Idea");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_IdeaPosts_IdeaPostId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_IdeaPosts_IdeaPostId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_IdeaPosts_IdeaPostId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "IdeaPosts");

            migrationBuilder.DropIndex(
                name: "IX_Project_IdeaPostId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Photos_IdeaPostId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Likes_IdeaPostId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Idea_IdeaPostId",
                table: "Idea");

            migrationBuilder.DropIndex(
                name: "IX_Comment_IdeaPostId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "IdeaPostId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IdeaPostId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "IdeaPostId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "IdeaPostId",
                table: "Idea");

            migrationBuilder.DropColumn(
                name: "IdeaPostId",
                table: "Comment");
        }
    }
}
