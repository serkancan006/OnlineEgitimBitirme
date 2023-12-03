using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseViewCount",
                table: "WidgetClickLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6aed193-c506-418e-842e-c3d1a2dc5e53", "AQAAAAIAAYagAAAAEEXw5YDc5MounBnxW7wCf1Xtc4vTULkd1k3zf0T4A6Hqx/7z02RAXgbG6IfnqQvTyw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseViewCount",
                table: "WidgetClickLogs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1c38224-6de3-439c-8968-18b06f721597", "AQAAAAIAAYagAAAAEDvsIXQEDoQGnDFuWV/TFGFTNEbAIUcZYC2QZ9nzeOIeT3Jf6SzDs6mmtdrWxIcjGA==" });
        }
    }
}
