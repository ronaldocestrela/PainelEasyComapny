using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Bookmakers_BookmakerId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Projects_ProjectId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Campaigns_CampaignId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_CampaignId",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Campaigns",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Campaigns",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Bookmakers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bookmakers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CampaignId_ReportDate",
                table: "Reports",
                columns: new[] { "CampaignId", "ReportDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Report_Currency",
                table: "Reports",
                column: "Currency");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReportDate",
                table: "Reports",
                column: "ReportDate");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_Name_ProjectId",
                table: "Campaigns",
                columns: new[] { "Name", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmaker_Name_Unique",
                table: "Bookmakers",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Bookmakers_BookmakerId",
                table: "Campaigns",
                column: "BookmakerId",
                principalTable: "Bookmakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Projects_ProjectId",
                table: "Campaigns",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Campaigns_CampaignId",
                table: "Reports",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Bookmakers_BookmakerId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Projects_ProjectId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Campaigns_CampaignId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Report_CampaignId_ReportDate",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Report_Currency",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Report_ReportDate",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Campaign_Name_ProjectId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Bookmaker_Name_Unique",
                table: "Bookmakers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Bookmakers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bookmakers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CampaignId",
                table: "Reports",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Bookmakers_BookmakerId",
                table: "Campaigns",
                column: "BookmakerId",
                principalTable: "Bookmakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Projects_ProjectId",
                table: "Campaigns",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Campaigns_CampaignId",
                table: "Reports",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
