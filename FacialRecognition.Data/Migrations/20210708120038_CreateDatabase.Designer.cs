﻿// <auto-generated />
using FacialRecognition.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FacialRecognition.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210708120038_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourseLecturer", b =>
                {
                    b.Property<string>("CoursesCourseCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LecturersStaffID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CoursesCourseCode", "LecturersStaffID");

                    b.HasIndex("LecturersStaffID");

                    b.ToTable("CourseLecturer");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<string>("CoursesCourseCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentsRegistrationNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CoursesCourseCode", "StudentsRegistrationNumber");

                    b.HasIndex("StudentsRegistrationNumber");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("FacialRecognition.Data.Models.Course", b =>
                {
                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CourseTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseCode");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("FacialRecognition.Data.Models.Lecturer", b =>
                {
                    b.Property<string>("StaffID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffID");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("FacialRecognition.Data.Models.Student", b =>
                {
                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegistrationNumber");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CourseLecturer", b =>
                {
                    b.HasOne("FacialRecognition.Data.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourseCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FacialRecognition.Data.Models.Lecturer", null)
                        .WithMany()
                        .HasForeignKey("LecturersStaffID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("FacialRecognition.Data.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourseCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FacialRecognition.Data.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsRegistrationNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
