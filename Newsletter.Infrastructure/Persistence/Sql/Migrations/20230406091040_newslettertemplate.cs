using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Newsletter.Infrastructure.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class newslettertemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsletterTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Html = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tokens = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsletterTemplates_Titels_TitelId",
                        column: x => x.TitelId,
                        principalTable: "Titels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterTemplates_TitelId",
                table: "NewsletterTemplates",
                column: "TitelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsletterTemplates");
        }
    }
}
