using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeworkModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_BulkInsertHomework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = File.ReadAllText(@"..\HomeworkModule\HomeworkModule.Infrastructure\Migrations\20251009101822_Add_BulkInsertHomework\Up.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = File.ReadAllText(@"..\HomeworkModule\HomeworkModule.Infrastructure\Migrations\20251009101822_Add_BulkInsertHomework\Down.sql");
            migrationBuilder.Sql(sql);
        }
    }
}
