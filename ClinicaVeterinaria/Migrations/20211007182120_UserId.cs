using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinaria.Migrations
{
    public partial class UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AspNetUsers_UserId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_VetAppointments_AspNetUsers_UserId",
                table: "VetAppointments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Animals",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_UserId",
                table: "Animals",
                newName: "IX_Animals_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "VetName",
                table: "VetAppointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "VetAppointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AnimalName",
                table: "VetAppointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AspNetUsers_UserID",
                table: "Animals",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Animals_AspNetUsers_UserID",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_VetAppointments_AspNetUsers_UserId",
                table: "VetAppointments");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Animals",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_UserID",
                table: "Animals",
                newName: "IX_Animals_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "VetName",
                table: "VetAppointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "VetAppointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnimalName",
                table: "VetAppointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AspNetUsers_UserId",
                table: "Animals",
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
    }
}
