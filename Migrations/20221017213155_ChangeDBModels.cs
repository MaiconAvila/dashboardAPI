using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DashboardAPI.Migrations
{
    public partial class ChangeDBModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdOrder",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "IdProduct",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IdOrder",
                table: "Demand");

            migrationBuilder.DropColumn(
                name: "IdTeam",
                table: "Demand");

            migrationBuilder.AddColumn<int>(
                name: "DemandId",
                table: "Team",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Product",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Order",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "DemandId",
                table: "Order",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Order",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_DemandId",
                table: "Team",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderId",
                table: "Product",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DemandId",
                table: "Order",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_TeamId",
                table: "Order",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Demand_DemandId",
                table: "Order",
                column: "DemandId",
                principalTable: "Demand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Team_TeamId",
                table: "Order",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Order_OrderId",
                table: "Product",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Demand_DemandId",
                table: "Team",
                column: "DemandId",
                principalTable: "Demand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Demand_DemandId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Team_TeamId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Order_OrderId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Demand_DemandId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_DemandId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Product_OrderId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Order_DemandId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_TeamId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DemandId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DemandId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "IdOrder",
                table: "Team",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Order",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProduct",
                table: "Order",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdOrder",
                table: "Demand",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTeam",
                table: "Demand",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
