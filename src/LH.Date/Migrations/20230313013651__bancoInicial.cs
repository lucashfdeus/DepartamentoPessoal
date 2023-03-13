using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LH.Date.Migrations
{
    public partial class _bancoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NomeDepartamento = table.Column<string>(type: "varchar(200)", nullable: false),
                    MesVigencia = table.Column<string>(type: "varchar(100)", nullable: false),
                    AnoVigencia = table.Column<int>(nullable: false),
                    TotalPagar = table.Column<decimal>(nullable: false),
                    TotalDescontos = table.Column<decimal>(nullable: false),
                    TotalExtras = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistroPontos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Codigo = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    ValorHora = table.Column<decimal>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Entrada = table.Column<DateTime>(nullable: false),
                    Saida = table.Column<DateTime>(nullable: false),
                    Almoco = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroPontos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DepartamentoId = table.Column<Guid>(nullable: false),
                    Codigo = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    TotalReceber = table.Column<decimal>(nullable: false),
                    HorasExtras = table.Column<double>(nullable: false),
                    HorasDebito = table.Column<double>(nullable: false),
                    DiasFalta = table.Column<int>(nullable: false),
                    DiasExtras = table.Column<int>(nullable: false),
                    DiasTrabalhados = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_DepartamentoId",
                table: "Funcionarios",
                column: "DepartamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "RegistroPontos");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
