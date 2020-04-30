using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class ImgProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c2847a90-145e-4eed-8ad5-eedebdaec768", "AQAAAAEAACcQAAAAEBzTRJf4+3fQG2BOxamMDIk4zn4Vn1vlS8uL740ZZpkx98Q0ZcdnT2mt8IB+GVlc2g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b1f4f009-a260-4abe-959a-0eb259690857", "AQAAAAEAACcQAAAAEIIeTvvRF7Z3UZOaewTr9jMUhTPZhNac/bjyu/2roBeg7u723F9nfuowCh+NT0rgiw==" });
        }
    }
}
