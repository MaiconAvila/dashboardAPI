using Microsoft.EntityFrameworkCore.Migrations;

namespace DashboardAPI.Migrations
{
    public partial class ChangeModelOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTeam");

            migrationBuilder.AddColumn<int>(
                name: "IdTeam",
                table: "Order",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameTeam",
                table: "Order",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Order",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_TeamId",
                table: "Order",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Team_TeamId",
                table: "Order",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Team_TeamId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_TeamId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IdTeam",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "NameTeam",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Order");

            migrationBuilder.CreateTable(
                name: "OrderTeam",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTeam", x => new { x.OrderId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_OrderTeam_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTeam_TeamId",
                table: "OrderTeam",
                column: "TeamId");
        }
    }
}
