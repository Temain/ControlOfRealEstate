using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlOfRealEstate.DataAccess.Migrations
{
    public partial class ChangeNameOfTableForumThread : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumThread_IllegalObjects_IllegalObjectId",
                table: "ForumThread");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumThread",
                table: "ForumThread");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumThreads",
                table: "ForumThread",
                column: "ForumThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumThreads_IllegalObjects_IllegalObjectId",
                table: "ForumThread",
                column: "IllegalObjectId",
                principalTable: "IllegalObjects",
                principalColumn: "IllegalObjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_ForumThread_IllegalObjectId",
                table: "ForumThread",
                newName: "IX_ForumThreads_IllegalObjectId");

            migrationBuilder.RenameTable(
                name: "ForumThread",
                newName: "ForumThreads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumThreads_IllegalObjects_IllegalObjectId",
                table: "ForumThreads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumThreads",
                table: "ForumThreads");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumThread",
                table: "ForumThreads",
                column: "ForumThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumThread_IllegalObjects_IllegalObjectId",
                table: "ForumThreads",
                column: "IllegalObjectId",
                principalTable: "IllegalObjects",
                principalColumn: "IllegalObjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_ForumThreads_IllegalObjectId",
                table: "ForumThreads",
                newName: "IX_ForumThread_IllegalObjectId");

            migrationBuilder.RenameTable(
                name: "ForumThreads",
                newName: "ForumThread");
        }
    }
}
