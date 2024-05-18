using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlePedido.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddColunaValorEmPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Pedidos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Pedidos");
        }
    }
}
