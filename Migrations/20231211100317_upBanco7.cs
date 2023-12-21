using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCC.Migrations
{
    /// <inheritdoc />
    public partial class upBanco7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Contas_ContaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ContaId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "ContaId",
                table: "Usuarios",
                newName: "IdConta");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "Notas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "EmailUsuario", "IdConta", "NomeUsuario", "Senha" },
                values: new object[] { 1, "koio@email.com", 1, "Koios", "123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "IdConta",
                table: "Usuarios",
                newName: "ContaId");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "Notas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ContaId",
                table: "Usuarios",
                column: "ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Contas_ContaId",
                table: "Usuarios",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
