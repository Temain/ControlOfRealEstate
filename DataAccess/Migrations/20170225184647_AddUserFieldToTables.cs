using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlOfRealEstate.DataAccess.Migrations
{
    public partial class AddUserFieldToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "IllegalObjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ForumThreads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IllegalObjects_ApplicationUserId",
                table: "IllegalObjects",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumThreads_ApplicationUserId",
                table: "ForumThreads",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumThreads_AspNetUsers_ApplicationUserId",
                table: "ForumThreads",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IllegalObjects_AspNetUsers_ApplicationUserId",
                table: "IllegalObjects",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumThreads_AspNetUsers_ApplicationUserId",
                table: "ForumThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_IllegalObjects_AspNetUsers_ApplicationUserId",
                table: "IllegalObjects");

            migrationBuilder.DropIndex(
                name: "IX_IllegalObjects_ApplicationUserId",
                table: "IllegalObjects");

            migrationBuilder.DropIndex(
                name: "IX_ForumThreads_ApplicationUserId",
                table: "ForumThreads");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "IllegalObjects");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ForumThreads");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Comments");
        }
    }
}
