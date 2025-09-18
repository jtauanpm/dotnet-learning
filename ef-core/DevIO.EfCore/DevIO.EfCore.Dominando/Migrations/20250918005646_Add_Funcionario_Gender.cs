using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIO.EfCore.Dominando.Migrations
{
    /// <inheritdoc />
    public partial class Add_Funcionario_Gender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeparmentoId",
                table: "Funcionarios");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Funcionarios",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Funcionarios");

            migrationBuilder.AddColumn<int>(
                name: "DeparmentoId",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
