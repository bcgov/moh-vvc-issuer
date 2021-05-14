using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Issuer.Migrations
{
    public partial class UpdatePatientModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "GivenNames",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PreferredFirstName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PreferredLastName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PreferredMiddleName",
                table: "Patient");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Patient",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Patient",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Patient");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Patient",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Patient",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GivenNames",
                table: "Patient",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Patient",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Patient",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredFirstName",
                table: "Patient",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredLastName",
                table: "Patient",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredMiddleName",
                table: "Patient",
                type: "text",
                nullable: true);
        }
    }
}
