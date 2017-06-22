using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayStore.Migrations
{
    public partial class play7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Android",
                table: "Compatibilities");

            migrationBuilder.DropColumn(
                name: "Others",
                table: "Compatibilities");

            migrationBuilder.DropColumn(
                name: "WinPhone",
                table: "Compatibilities");

            migrationBuilder.AddColumn<string>(
                name: "DeviceType",
                table: "Compatibilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "Compatibilities");

            migrationBuilder.AddColumn<byte[]>(
                name: "Android",
                table: "Compatibilities",
                type: "binary(1)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "Others",
                table: "Compatibilities",
                type: "varchar(2500)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "WinPhone",
                table: "Compatibilities",
                type: "binary(1)",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
