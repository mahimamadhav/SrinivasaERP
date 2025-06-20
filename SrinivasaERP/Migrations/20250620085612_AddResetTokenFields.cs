﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SrinivasaERP.Migrations
{
    /// <inheritdoc />
    public partial class AddResetTokenFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "Registers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpiry",
                table: "Registers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpiry",
                table: "Registers");
        }
    }
}
