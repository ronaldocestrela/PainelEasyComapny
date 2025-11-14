using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectIdToCampaign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "Campaigns",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ProjectId",
                table: "Campaigns",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Projects_ProjectId",
                table: "Campaigns",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Projects_ProjectId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_ProjectId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Campaigns");
        }
    }
}
