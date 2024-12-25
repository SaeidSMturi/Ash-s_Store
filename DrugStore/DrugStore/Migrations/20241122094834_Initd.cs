using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrugStore.Migrations
{
    /// <inheritdoc />
    public partial class Initd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivePhoneNumberCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsPhoneNumberActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "LastChange", "RegisterDate" },
                values: new object[] { new DateTime(2024, 11, 22, 13, 18, 34, 388, DateTimeKind.Local).AddTicks(704), new DateTime(2024, 11, 22, 13, 18, 34, 388, DateTimeKind.Local).AddTicks(716) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ActivePhoneNumberCode",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPhoneNumberActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ActivePhoneNumberCode", "IsPhoneNumberActive", "LastChange", "PhoneNumber", "RegisterDate" },
                values: new object[] { "12345", true, new DateTime(2024, 11, 22, 12, 33, 4, 849, DateTimeKind.Local).AddTicks(1428), "xxxxxxxxxxx", new DateTime(2024, 11, 22, 12, 33, 4, 849, DateTimeKind.Local).AddTicks(1438) });
        }
    }
}
