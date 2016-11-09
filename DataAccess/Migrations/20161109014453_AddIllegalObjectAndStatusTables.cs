using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlOfRealEstate.DataAccess.Migrations
{
    public partial class AddIllegalObjectAndStatusTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IllegalObjects",
                columns: table => new
                {
                    IllegalObjectId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Infringement = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NeagentId = table.Column<int>(nullable: true),
                    ResultsOfReview = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IllegalObjects", x => x.IllegalObjectId);
                    table.ForeignKey(
                        name: "FK_IllegalObjects_IllegalObjectStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "IllegalObjectStatuses",
                        principalColumn: "IllegalObjectStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IllegalObjects_StatusId",
                table: "IllegalObjects",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IllegalObjects");
        }
    }
}
