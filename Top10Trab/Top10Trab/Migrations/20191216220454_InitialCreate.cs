using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Top10Trab.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jogadores",
                columns: table => new
                {
                    JogadorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Idade = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Nacionalidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogadores", x => x.JogadorID);
                });

            migrationBuilder.CreateTable(
                name: "Placares",
                columns: table => new
                {
                    PlacarID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JogadorID = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Pontuacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placares", x => x.PlacarID);
                    table.ForeignKey(
                        name: "FK_Placares_Jogadores_JogadorID",
                        column: x => x.JogadorID,
                        principalTable: "Jogadores",
                        principalColumn: "JogadorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Placares_JogadorID",
                table: "Placares",
                column: "JogadorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Placares");

            migrationBuilder.DropTable(
                name: "Jogadores");
        }
    }
}
