﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    ArticuloId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Costo = table.Column<decimal>(type: "TEXT", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    Existencia = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.ArticuloId);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombres = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    WhatsApp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Prioridades",
                columns: table => new
                {
                    PrioridadId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Tiempo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridades", x => x.PrioridadId);
                });

            migrationBuilder.CreateTable(
                name: "TiposTecnicos",
                columns: table => new
                {
                    TipoTecnicoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposTecnicos", x => x.TipoTecnicoId);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    TecnicoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SueldoHora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoTecnicoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.TecnicoId);
                    table.ForeignKey(
                        name: "FK_Tecnicos_TiposTecnicos_TipoTecnicoId",
                        column: x => x.TipoTecnicoId,
                        principalTable: "TiposTecnicos",
                        principalColumn: "TipoTecnicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trabajos",
                columns: table => new
                {
                    TrabajoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    TecnicoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrioridadId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajos", x => x.TrabajoId);
                    table.ForeignKey(
                        name: "FK_Trabajos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajos_Prioridades_PrioridadId",
                        column: x => x.PrioridadId,
                        principalTable: "Prioridades",
                        principalColumn: "PrioridadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajos_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "TecnicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrabajosDetalle",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrabajosId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticuloId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    Costo = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajosDetalle", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_TrabajosDetalle_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "ArticuloId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajosDetalle_Trabajos_TrabajosId",
                        column: x => x.TrabajosId,
                        principalTable: "Trabajos",
                        principalColumn: "TrabajoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Articulos",
                columns: new[] { "ArticuloId", "Costo", "Descripcion", "Existencia", "Precio" },
                values: new object[,]
                {
                    { 1, 300.0m, "RTX 4060", 15m, 400.0m },
                    { 2, 450.0m, "RTX 4070", 10m, 600.0m },
                    { 3, 650.0m, "RTX 4070 Ti", 8m, 800.0m },
                    { 4, 900.0m, "RTX 4080", 5m, 1200.0m },
                    { 5, 1600.0m, "RTX 4090", 3m, 2100.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_TipoTecnicoId",
                table: "Tecnicos",
                column: "TipoTecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajos_ClienteId",
                table: "Trabajos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajos_PrioridadId",
                table: "Trabajos",
                column: "PrioridadId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajos_TecnicoId",
                table: "Trabajos",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajosDetalle_ArticuloId",
                table: "TrabajosDetalle",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajosDetalle_TrabajosId",
                table: "TrabajosDetalle",
                column: "TrabajosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrabajosDetalle");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "Trabajos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Prioridades");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "TiposTecnicos");
        }
    }
}
