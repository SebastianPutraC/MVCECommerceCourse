using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCECommerceCourseWeb.Migrations;

/// <inheritdoc />
public partial class _20261123114302_UpdateIdInCategory : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "CatgeoryIndentification",
            table: "Categories",
            newName: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Id",
            table: "Categories",
            newName: "CatgeoryIndentification");
    }
}
