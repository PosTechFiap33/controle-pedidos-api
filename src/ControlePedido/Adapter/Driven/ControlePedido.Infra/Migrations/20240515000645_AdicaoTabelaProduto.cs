using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlePedido.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoTabelaProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Categoria = table.Column<int>(type: "integer", nullable: false),
                    UrlImagem = table.Column<string>(type: "text", nullable: false),
                    ExtensaoImagem = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    NomeImagem = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
