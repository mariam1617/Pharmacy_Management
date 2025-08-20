using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "201",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "939823ac-b96f-4b77-9476-1351ec0b0a1e", "d546029a-6122-417c-9cca-ebf56820ffb8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "202",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6959862c-00dd-415e-a014-a13c16cab8ae", "bab5cf90-762e-4d24-a23b-ebd807c5f85c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "203",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c61b0dab-43e3-477f-8695-0f7898ce5812", "d5c8957e-7eb2-4da1-822c-cf2710d175f5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "201",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7a5eaf07-3a47-4117-82c6-1f33d538dd1f", "4be1c6d8-981f-4966-a1e1-364b20be4da2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "202",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2f2982cc-b98b-430d-a855-f26f77040269", "c9576341-fef7-4f9d-8a10-1be23597443d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "203",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e1b6a773-f317-4a51-9b6e-ba37ff495f39", "71616049-efbd-4072-916c-542150b4469f" });
        }
    }
}
