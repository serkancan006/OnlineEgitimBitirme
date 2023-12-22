using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Profession" },
                values: new object[] { "8f8450ed-dc95-4e3c-bfac-02be6116ee64", "AQAAAAIAAYagAAAAEBa11CwdUcfnVrap7ZwZOszwQ1a6p1EWfgbeT+vq1mMjxovOBCGirviQlXJNoDYMhg==", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6aed193-c506-418e-842e-c3d1a2dc5e53", "AQAAAAIAAYagAAAAEEXw5YDc5MounBnxW7wCf1Xtc4vTULkd1k3zf0T4A6Hqx/7z02RAXgbG6IfnqQvTyw==" });
        }
    }
}
