using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class updatingresponsemodelagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Weddings_WeddingId",
                table: "Responses");

            migrationBuilder.AlterColumn<int>(
                name: "WeddingId",
                table: "Responses",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Weddings_WeddingId",
                table: "Responses",
                column: "WeddingId",
                principalTable: "Weddings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Weddings_WeddingId",
                table: "Responses");

            migrationBuilder.AlterColumn<int>(
                name: "WeddingId",
                table: "Responses",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Weddings_WeddingId",
                table: "Responses",
                column: "WeddingId",
                principalTable: "Weddings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
