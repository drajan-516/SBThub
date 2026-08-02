using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBThub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameCreatedByUserIdToCreatedByUserUuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Products",
                newName: "UserUuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserUuid",
                table: "Products",
                newName: "CreatedByUserId");
        }
    }
}
