using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHintCountToSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HintCount",
                table: "ChatSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HintCount",
                table: "ChatSessions");
        }
    }
}
