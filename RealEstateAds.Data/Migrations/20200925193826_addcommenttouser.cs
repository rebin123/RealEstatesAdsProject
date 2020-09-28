using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAds.Data.Migrations
{
    public partial class addcommenttouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RealEstatesC",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RealEstateId1",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RealEstateId1",
                table: "Comments",
                column: "RealEstateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_RealEstates_RealEstateId1",
                table: "Comments",
                column: "RealEstateId1",
                principalTable: "RealEstates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_RealEstates_RealEstateId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_RealEstateId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RealEstateId1",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "Comments",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RealEstatesC",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
