using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIO.EfCore.Dominando.Migrations
{
    /// <inheritdoc />
    public partial class Add_Funcionario_ContractType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "Funcionarios");
        }
    }
}
