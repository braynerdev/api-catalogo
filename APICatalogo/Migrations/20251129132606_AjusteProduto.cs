using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class AjusteProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_CategoriaId",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Produtos",
                newName: "categoria_id");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                newName: "IX_Produtos_categoria_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_categoria_id",
                table: "Produtos",
                column: "categoria_id",
                principalTable: "Categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_categoria_id",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "categoria_id",
                table: "Produtos",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_categoria_id",
                table: "Produtos",
                newName: "IX_Produtos_CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_CategoriaId",
                table: "Produtos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
