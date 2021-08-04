using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacialRecognition.Data.Migrations
{
    public partial class StudentImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "StudentImage",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentImage",
                table: "Students");
        }
    }
}
