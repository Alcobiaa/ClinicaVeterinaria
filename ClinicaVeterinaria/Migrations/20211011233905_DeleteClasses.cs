using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinaria.Migrations
{
    public partial class DeleteClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDetails");

            migrationBuilder.DropTable(
                name: "AppointmentDetailsTemp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Room = table.Column<int>(type: "int", nullable: false),
                    VetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentDetails_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppointmentDetails_Vets_VetId",
                        column: x => x.VetId,
                        principalTable: "Vets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentDetailsTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Room = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDetailsTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentDetailsTemp_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppointmentDetailsTemp_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppointmentDetailsTemp_Vets_VetId",
                        column: x => x.VetId,
                        principalTable: "Vets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetails_AnimalId",
                table: "AppointmentDetails",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetails_VetId",
                table: "AppointmentDetails",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetailsTemp_AnimalId",
                table: "AppointmentDetailsTemp",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetailsTemp_UserId",
                table: "AppointmentDetailsTemp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetailsTemp_VetId",
                table: "AppointmentDetailsTemp",
                column: "VetId");
        }
    }
}
