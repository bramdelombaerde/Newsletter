using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Newsletter.Infrastructure.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class newsletter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Newsletters_Titels_TitelId",
                        column: x => x.TitelId,
                        principalTable: "Titels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Newsletters_TitelId",
                table: "Newsletters",
                column: "TitelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Newsletters");
        }
    }
}
