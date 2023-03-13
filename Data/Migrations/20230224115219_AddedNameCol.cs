﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace birdssweden300.web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Birds",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Birds");
        }
    }
}
