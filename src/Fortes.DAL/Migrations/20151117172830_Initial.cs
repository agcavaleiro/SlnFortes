using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Fortes.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CategoriaId = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesa_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CategoriaId = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receita_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Despesa");
            migrationBuilder.DropTable("Receita");
            migrationBuilder.DropTable("Categoria");
        }
    }
}
