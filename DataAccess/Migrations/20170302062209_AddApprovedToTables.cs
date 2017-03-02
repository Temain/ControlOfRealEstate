using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlOfRealEstate.DataAccess.Migrations
{
    public partial class AddApprovedToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "IllegalObjects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "ForumThreads",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "IllegalObjects");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "ForumThreads");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Comments");
        }
    }
}
