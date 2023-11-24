using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_CourseID",
                table: "PurchasedCourses",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedCourses_Courses_CourseID",
                table: "PurchasedCourses",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedCourses_Courses_CourseID",
                table: "PurchasedCourses");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedCourses_CourseID",
                table: "PurchasedCourses");
        }
    }
}
