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
                });
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DespesaId = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    ReceitaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_Despesa_DespesaId",
                        column: x => x.DespesaId,
                        principalTable: "Despesa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categoria_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Categoria");
            migrationBuilder.DropTable("Despesa");
            migrationBuilder.DropTable("Receita");
        }
    }
}
