using Microsoft.EntityFrameworkCore.Migrations;

namespace FacialRecognition.Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseCode);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    StaffID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.StaffID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    RegistrationNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.RegistrationNumber);
                });

            migrationBuilder.CreateTable(
                name: "CourseLecturer",
                columns: table => new
                {
                    CoursesCourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LecturersStaffID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLecturer", x => new { x.CoursesCourseCode, x.LecturersStaffID });
                    table.ForeignKey(
                        name: "FK_CourseLecturer_Courses_CoursesCourseCode",
                        column: x => x.CoursesCourseCode,
                        principalTable: "Courses",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseLecturer_Lecturers_LecturersStaffID",
                        column: x => x.LecturersStaffID,
                        principalTable: "Lecturers",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CoursesCourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentsRegistrationNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesCourseCode, x.StudentsRegistrationNumber });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Courses_CoursesCourseCode",
                        column: x => x.CoursesCourseCode,
                        principalTable: "Courses",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_Students_StudentsRegistrationNumber",
                        column: x => x.StudentsRegistrationNumber,
                        principalTable: "Students",
                        principalColumn: "RegistrationNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturer_LecturersStaffID",
                table: "CourseLecturer",
                column: "LecturersStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsRegistrationNumber",
                table: "CourseStudent",
                column: "StudentsRegistrationNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLecturer");

            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
