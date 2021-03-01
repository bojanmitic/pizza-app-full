using Microsoft.EntityFrameworkCore.Migrations;

namespace pizza_server.Migrations
{
    public partial class MealAndMealSectionRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealSections_MealSectionId",
                table: "Meals");

            migrationBuilder.AlterColumn<int>(
                name: "MealSectionId",
                table: "Meals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealSections_MealSectionId",
                table: "Meals",
                column: "MealSectionId",
                principalTable: "MealSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealSections_MealSectionId",
                table: "Meals");

            migrationBuilder.AlterColumn<int>(
                name: "MealSectionId",
                table: "Meals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealSections_MealSectionId",
                table: "Meals",
                column: "MealSectionId",
                principalTable: "MealSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
