using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlOfRealEstate.DataAccess.Migrations
{
    public partial class AddOrderToIllegalObjectStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IllegalObjectStatusOrder",
                table: "IllegalObjectStatuses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IllegalObjectStatusOrder",
                table: "IllegalObjectStatuses");
        }
    }
}
