using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlOfRealEstate.DataAccess.Migrations
{
    public partial class AddDateTimeFieldToIllegalObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "IllegalObjects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "IllegalObjects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "IllegalObjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "IllegalObjects");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "IllegalObjects");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "IllegalObjects");
        }
    }
}
