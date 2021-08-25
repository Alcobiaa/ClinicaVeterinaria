using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinaria.Migrations
{
    public partial class AddUserr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vets_UserId",
                table: "Vets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vets_AspNetUsers_UserId",
                table: "Vets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vets_AspNetUsers_UserId",
                table: "Vets");

            migrationBuilder.DropIndex(
                name: "IX_Vets_UserId",
                table: "Vets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vets");
        }
    }
}
