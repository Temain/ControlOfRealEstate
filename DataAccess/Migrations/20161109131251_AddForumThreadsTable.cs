using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlOfRealEstate.DataAccess.Migrations
{
    public partial class AddForumThreadsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumThread",
                columns: table => new
                {
                    ForumThreadId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IllegalObjectId = table.Column<int>(nullable: false),
                    Theme = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumThread", x => x.ForumThreadId);
                    table.ForeignKey(
                        name: "FK_ForumThread_IllegalObjects_IllegalObjectId",
                        column: x => x.IllegalObjectId,
                        principalTable: "IllegalObjects",
                        principalColumn: "IllegalObjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumThread_IllegalObjectId",
                table: "ForumThread",
                column: "IllegalObjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumThread");
        }
    }
}
