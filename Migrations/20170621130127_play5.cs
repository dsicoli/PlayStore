using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayStore.Migrations
{
    public partial class play5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Successful",
                table: "Downloads",
                type: "binary(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "binary(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Successful",
                table: "Downloads",
                type: "binary(1)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(1)",
                oldNullable: true);
        }
    }
}
