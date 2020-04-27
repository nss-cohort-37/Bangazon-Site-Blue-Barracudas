using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class localDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5038bd64-0d05-4341-ad8b-7217dca07b7d", "AQAAAAEAACcQAAAAEK+Clb+4EofnpOGlySx4DdUqBgKfbHHzW4tJ6osAaZGb1PZBVFB68ptDinrAY0DSPg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e086d6ae-d4b7-419a-aa7f-4d2bef6a1f00", "AQAAAAEAACcQAAAAEMElw2dGmN2XaQbqNltjNmYKsvbn9Flu1Df8zFgzbPInXoKLtvYQYo5Oep2sfEeFhw==" });
        }
    }
}
