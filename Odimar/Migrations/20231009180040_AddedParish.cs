using Microsoft.EntityFrameworkCore.Migrations;

namespace Odimar.Migrations
{
    public partial class AddedParish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParishId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Parishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parishes_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counties_Name",
                table: "Counties",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParishId",
                table: "AspNetUsers",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX_Parishes_CountyId",
                table: "Parishes",
                column: "CountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Parishes_ParishId",
                table: "AspNetUsers",
                column: "ParishId",
                principalTable: "Parishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Parishes_ParishId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Parishes");

            migrationBuilder.DropIndex(
                name: "IX_Counties_Name",
                table: "Counties");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParishId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParishId",
                table: "AspNetUsers");
        }
    }
}
