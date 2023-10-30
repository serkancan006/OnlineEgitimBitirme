using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_AppUserID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Locations_LocationID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedCourses_AspNetUsers_AppUserID",
                table: "PurchasedCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedCourses_Courses_CourseID",
                table: "PurchasedCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_WidgetClickLogs_AspNetUsers_AppUserID",
                table: "WidgetClickLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WidgetClickLogs_Courses_CourseID",
                table: "WidgetClickLogs");

            migrationBuilder.DropIndex(
                name: "IX_WidgetClickLogs_AppUserID",
                table: "WidgetClickLogs");

            migrationBuilder.DropIndex(
                name: "IX_WidgetClickLogs_CourseID",
                table: "WidgetClickLogs");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedCourses_AppUserID",
                table: "PurchasedCourses");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedCourses_CourseID",
                table: "PurchasedCourses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AppUserID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LocationID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Courses",
                newName: "SubjectCount");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "SubjectCount",
                table: "Courses",
                newName: "LocationID");

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WidgetClickLogs_AppUserID",
                table: "WidgetClickLogs",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetClickLogs_CourseID",
                table: "WidgetClickLogs",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_AppUserID",
                table: "PurchasedCourses",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_CourseID",
                table: "PurchasedCourses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AppUserID",
                table: "Courses",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LocationID",
                table: "Courses",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_AppUserID",
                table: "Courses",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Locations_LocationID",
                table: "Courses",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedCourses_AspNetUsers_AppUserID",
                table: "PurchasedCourses",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedCourses_Courses_CourseID",
                table: "PurchasedCourses",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetClickLogs_AspNetUsers_AppUserID",
                table: "WidgetClickLogs",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetClickLogs_Courses_CourseID",
                table: "WidgetClickLogs",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
