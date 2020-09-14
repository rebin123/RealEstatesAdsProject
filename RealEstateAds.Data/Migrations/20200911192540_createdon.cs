using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAds.Data.Migrations
{
    public partial class createdon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "RealEstates");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "RealEstates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "RealEstates");

            migrationBuilder.AddColumn<DateTime>(
                name: "Published",
                table: "RealEstates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
