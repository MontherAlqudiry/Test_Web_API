using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class addDemandOneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "demandOneText",
                table: "ComplaintsApp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "demandOne",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    demandOneText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demandOne", x => x.Id);
                    table.ForeignKey(
                        name: "FK_demandOne_ComplaintsApp_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "ComplaintsApp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_demandOne_ComplaintId",
                table: "demandOne",
                column: "ComplaintId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "demandOne");

            migrationBuilder.DropColumn(
                name: "demandOneText",
                table: "ComplaintsApp");
        }
    }
}
