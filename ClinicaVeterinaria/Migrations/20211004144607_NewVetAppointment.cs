using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinaria.Migrations
{
    public partial class NewVetAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "VetAppointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VetAppointments_UserId",
                table: "VetAppointments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VetAppointments_AspNetUsers_UserId",
                table: "VetAppointments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VetAppointments_AspNetUsers_UserId",
                table: "VetAppointments");

            migrationBuilder.DropIndex(
                name: "IX_VetAppointments_UserId",
                table: "VetAppointments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VetAppointments");
        }
    }
}
