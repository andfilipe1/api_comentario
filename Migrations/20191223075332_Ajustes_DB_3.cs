using Microsoft.EntityFrameworkCore.Migrations;

namespace APIBlog.Migrations
{
    public partial class Ajustes_DB_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_Album",
                table: "Fotos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_Album",
                table: "Fotos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
