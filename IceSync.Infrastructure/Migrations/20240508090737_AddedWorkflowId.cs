using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IceSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkflowId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkflowId",
                table: "Workflows",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "Workflows");
        }
    }
}
