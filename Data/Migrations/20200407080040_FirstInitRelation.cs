using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class FirstInitRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presences_Classes_ClassId",
                table: "Presences");

            migrationBuilder.DropIndex(
                name: "IX_Presences_ClassId",
                table: "Presences");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Presences");

            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "Presences",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presences_ClassroomId",
                table: "Presences",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_Classrooms_ClassroomId",
                table: "Presences",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presences_Classrooms_ClassroomId",
                table: "Presences");

            migrationBuilder.DropIndex(
                name: "IX_Presences_ClassroomId",
                table: "Presences");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "Presences");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Presences",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presences_ClassId",
                table: "Presences",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_Classes_ClassId",
                table: "Presences",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
