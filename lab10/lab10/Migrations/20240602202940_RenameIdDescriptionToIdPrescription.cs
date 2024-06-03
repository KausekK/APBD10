using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab10.Migrations
{
    /// <inheritdoc />
    public partial class RenameIdDescriptionToIdPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdDescription",
                table: "Prescriptions",
                newName: "IdPrescription");

            migrationBuilder.RenameColumn(
                name: "BirthDay",
                table: "Patients",
                newName: "Birthdate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdPrescription",
                table: "Prescriptions",
                newName: "IdDescription");

            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Patients",
                newName: "BirthDay");
        }
    }
}
