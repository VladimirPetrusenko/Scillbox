using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillProfiWebApplication.Migrations
{
    public partial class UpdatePostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateOfPublication",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPublication",
                table: "Posts");
        }
    }
}
