using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class ModifyComplaintTableToIncludeUserGmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserGmail",
                table: "ComplaintsApp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserGmail",
                table: "ComplaintsApp");
        }
    }
}
