﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientBarometer.Migrator.Migrations.ClientBarometerDb
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Source = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RowVersion = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChatId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Text = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RowVersion = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Source = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Birthday = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RowVersion = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "chats",
                columns: new[] { "Id", "Source", "SourceId" },
                values: new object[] { new Guid("550eb2cc-6b25-4239-9db3-992ba5cbee08"), "Telegram", new Guid("550eb2cc-6b25-4239-9db3-992ba5cbee08") });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Birthday", "Name", "Source", "SourceId" },
                values: new object[] { new Guid("70313da4-aa68-41d3-bf80-265e2743846f"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Telegram", new Guid("70313da4-aa68-41d3-bf80-265e2743846f") });

            migrationBuilder.CreateIndex(
                name: "IX_chats_Id",
                table: "chats",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ChatId",
                table: "messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_Id",
                table: "messages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserId",
                table: "messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Id",
                table: "users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
