using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Files_CourseID",
                table: "Files",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Courses_CourseID",
                table: "Files",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Courses_CourseID",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CourseID",
                table: "Files");
        }
    }
}
