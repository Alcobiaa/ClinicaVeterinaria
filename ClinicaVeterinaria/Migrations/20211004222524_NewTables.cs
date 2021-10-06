using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinaria.Migrations
{
    public partial class NewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "Vet",
                table: "VetAppointments",
                newName: "VetName");

            migrationBuilder.RenameColumn(
                name: "Animal",
                table: "VetAppointments",
                newName: "AnimalName");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "VetAppointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "VetAppointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_OwnerId",
                table: "Animals",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Clients_OwnerId",
                table: "Animals",
                column: "OwnerId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Clients_OwnerId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_OwnerId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "VetAppointments");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "VetAppointments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "VetName",
                table: "VetAppointments",
                newName: "Vet");

            migrationBuilder.RenameColumn(
                name: "AnimalName",
                table: "VetAppointments",
                newName: "Animal");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
