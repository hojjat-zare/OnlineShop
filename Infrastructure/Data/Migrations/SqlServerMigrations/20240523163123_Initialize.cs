using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations.SqlServerMigrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CatalogItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogContent", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_CatalogContent_Catalog_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "Catalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "ID_CatalogContent_CatalogItemId",
                table: "CatalogContent",
                column: "CatalogItemId")
                .Annotation("SqlServer:Clustered", true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
