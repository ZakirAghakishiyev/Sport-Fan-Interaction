using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSelling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DetailsNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EncodedExpirationDate",
                table: "CardDetails",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "EncodedCvc",
                table: "CardDetails",
                newName: "Cvc");

            migrationBuilder.RenameColumn(
                name: "EncodedCardHolderName",
                table: "CardDetails",
                newName: "CardHolderName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "CardDetails",
                newName: "EncodedExpirationDate");

            migrationBuilder.RenameColumn(
                name: "Cvc",
                table: "CardDetails",
                newName: "EncodedCvc");

            migrationBuilder.RenameColumn(
                name: "CardHolderName",
                table: "CardDetails",
                newName: "EncodedCardHolderName");
        }
    }
}
