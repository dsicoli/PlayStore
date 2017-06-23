using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayStore.Migrations
{
    public partial class playplay1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apps",
                table: "Apps");

            migrationBuilder.CreateIndex(
                name: "IX_Apps",
                table: "Apps",
                column: "LastUpdate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apps",
                table: "Apps");

            migrationBuilder.CreateIndex(
                name: "IX_Apps",
                table: "Apps",
                column: "LastUpdate",
                unique: true);
        }
    }
}
