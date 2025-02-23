using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unityREST.Migrations
{
    /// <inheritdoc />
    public partial class AddIslandIdAndCharacterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IslandId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IslandId",
                table: "Players");
        }
    }
}
