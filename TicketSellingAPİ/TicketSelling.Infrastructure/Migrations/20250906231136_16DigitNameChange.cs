using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSelling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _16DigitNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Encoded16Digits",
                table: "CardDetails",
                newName: "All16Digits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "All16Digits",
                table: "CardDetails",
                newName: "Encoded16Digits");
        }
    }
}
