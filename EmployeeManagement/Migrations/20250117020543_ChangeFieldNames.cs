using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Supervisor",
                newName: "EMail");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Supervisor",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Employee",
                newName: "EMail");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Employee",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EMail",
                table: "Supervisor",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Supervisor",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "EMail",
                table: "Employee",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Employee",
                newName: "Phone");
        }
    }
}
