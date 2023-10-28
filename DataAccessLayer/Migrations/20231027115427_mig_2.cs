using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "WidgetClickLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PurchasedCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Abouts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_WidgetClickLogs_AppUserID",
                table: "WidgetClickLogs",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetClickLogs_CourseID",
                table: "WidgetClickLogs",
                column: "CourseID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WidgetClickLogs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PurchasedCourses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Abouts");
        }
    }
}
