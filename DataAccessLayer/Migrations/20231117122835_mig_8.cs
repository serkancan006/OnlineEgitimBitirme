using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseVideoFile");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseCourseVideoFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseVideoImageFile = table.Column<int>(type: "int", nullable: false),
                    CourseImageFileId = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseVideoFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCourseVideoFiles_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseVideoFiles_Files_CourseImageFileId",
                        column: x => x.CourseImageFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseVideoFiles_CourseID",
                table: "CourseCourseVideoFiles",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseVideoFiles_CourseImageFileId",
                table: "CourseCourseVideoFiles",
                column: "CourseImageFileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseVideoFiles");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Files");

            migrationBuilder.CreateTable(
                name: "CourseCourseVideoFile",
                columns: table => new
                {
                    CourseVideoFilesId = table.Column<int>(type: "int", nullable: false),
                    CoursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseVideoFile", x => new { x.CourseVideoFilesId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CourseCourseVideoFile_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseVideoFile_Files_CourseVideoFilesId",
                        column: x => x.CourseVideoFilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseVideoFile_CoursesId",
                table: "CourseCourseVideoFile",
                column: "CoursesId");
        }
    }
}
