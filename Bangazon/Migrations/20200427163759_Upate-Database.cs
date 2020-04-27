using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class UpateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "localDelivery",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e086d6ae-d4b7-419a-aa7f-4d2bef6a1f00", "AQAAAAEAACcQAAAAEMElw2dGmN2XaQbqNltjNmYKsvbn9Flu1Df8zFgzbPInXoKLtvYQYo5Oep2sfEeFhw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "localDelivery",
                table: "Product");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "90c396ca-6581-4870-a24b-71826f7aa11b", "AQAAAAEAACcQAAAAEKTPnZxwwGDffryB8AdBuqt0Eoy+MUDlmvgEKttD2qbJANSj/Gd+6ZpEjVX30SzKtg==" });
        }
    }
}
