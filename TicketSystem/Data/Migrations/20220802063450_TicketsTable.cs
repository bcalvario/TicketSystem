using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Data.Migrations
{
    public partial class TicketsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EncargadoNombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EncargadoApellido1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EncargadoApellido2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EncargadoCorreo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
