using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinaria.Migrations
{
    public partial class deleteuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VetAppointments_AspNetUsers_UserId",
                table: "VetAppointments");

            migrationBuilder.DropIndex(
                name: "IX_VetAppointments_UserId",
                table: "VetAppointments");

            migrationBuilder.DropColumn(
                name: "UserClientId",
                table: "VetAppointments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VetAppointments");

            migrationBuilder.DropColumn(
                name: "UserClientId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "VetAppointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "VetAppointments");

            migrationBuilder.AddColumn<int>(
                name: "UserClientId",
                table: "VetAppointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "VetAppointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserClientId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
