using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinaria.Migrations
{
    public partial class vetAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "VetAppointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AppointmentDetailsTemp",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VetAppointments_UserId",
                table: "VetAppointments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetailsTemp_UserId",
                table: "AppointmentDetailsTemp",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDetailsTemp_AspNetUsers_UserId",
                table: "AppointmentDetailsTemp",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VetAppointments_AspNetUsers_UserId",
                table: "VetAppointments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDetailsTemp_AspNetUsers_UserId",
                table: "AppointmentDetailsTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_VetAppointments_AspNetUsers_UserId",
                table: "VetAppointments");

            migrationBuilder.DropIndex(
                name: "IX_VetAppointments_UserId",
                table: "VetAppointments");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentDetailsTemp_UserId",
                table: "AppointmentDetailsTemp");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VetAppointments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppointmentDetailsTemp");
        }
    }
}
