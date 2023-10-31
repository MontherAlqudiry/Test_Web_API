using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class modifydemandOneModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_demandOne_ComplaintsApp_ComplaintId",
                table: "demandOne");

            migrationBuilder.DropIndex(
                name: "IX_demandOne_ComplaintId",
                table: "demandOne");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_demandOne_ComplaintId",
                table: "demandOne",
                column: "ComplaintId");

            migrationBuilder.AddForeignKey(
                name: "FK_demandOne_ComplaintsApp_ComplaintId",
                table: "demandOne",
                column: "ComplaintId",
                principalTable: "ComplaintsApp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
