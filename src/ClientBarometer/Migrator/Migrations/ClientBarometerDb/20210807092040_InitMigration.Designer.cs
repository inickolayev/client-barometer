﻿// <auto-generated />
using System;
using ClientBarometer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClientBarometer.Migrator.Migrations.ClientBarometerDb
{
    [DbContext(typeof(ClientBarometerDbContext))]
    [Migration("20210807092040_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ClientBarometer.Domain.Models.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("chats");

                    b.HasData(
                        new
                        {
                            Id = new Guid("550eb2cc-6b25-4239-9db3-992ba5cbee08"),
                            Source = "Telegram",
                            SourceId = new Guid("550eb2cc-6b25-4239-9db3-992ba5cbee08")
                        });
                });

            modelBuilder.Entity("ClientBarometer.Domain.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("Id");

                    b.HasIndex("UserId");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("ClientBarometer.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("70313da4-aa68-41d3-bf80-265e2743846f"),
                            Birthday = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Admin",
                            Source = "Telegram",
                            SourceId = new Guid("70313da4-aa68-41d3-bf80-265e2743846f")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
