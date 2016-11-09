using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlOfRealEstate.DataAccess.Migrations
{
    public partial class AddIllegalObjectPlacemark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IllegalObjectPlacemark",
                table: "IllegalObjectStatuses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IllegalObjectPlacemark",
                table: "IllegalObjectStatuses");
        }
    }
}
