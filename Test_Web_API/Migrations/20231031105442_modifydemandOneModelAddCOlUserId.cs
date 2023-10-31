using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class modifydemandOneModelAddCOlUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "demandOne",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "demandOne");
        }
    }
}
