using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    public partial class AddMissingColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // DateOfBirth column ekleme
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AppUsers",
                type: "datetime2",
                nullable: true);

            // ProfilePicture column ekleme
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            // ApplicationForm column ekleme
            migrationBuilder.AddColumn<string>(
                name: "ApplicationForm",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // DateOfBirth column silme
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AppUsers");

            // ProfilePicture column silme
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AppUsers");

            // ApplicationForm column silme
            migrationBuilder.DropColumn(
                name: "ApplicationForm",
                table: "AppUsers");
        }
    }
}
