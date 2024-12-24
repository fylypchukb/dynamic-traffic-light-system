using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicTrafficLightServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthIdentityId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Intersections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateById = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intersections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intersections_Users_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrafficLights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntersectionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateById = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficLights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrafficLights_Intersections_IntersectionId",
                        column: x => x.IntersectionId,
                        principalTable: "Intersections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrafficLights_Users_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrafficLightId = table.Column<int>(type: "int", nullable: false),
                    MinGreenTime = table.Column<int>(type: "int", nullable: false),
                    MaxGreenTime = table.Column<int>(type: "int", nullable: false),
                    TimePerVehicle = table.Column<int>(type: "int", nullable: false),
                    SequenceGreenTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultGreenTime = table.Column<int>(type: "int", nullable: false),
                    DefaultRedTime = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateById = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_TrafficLights_TrafficLightId",
                        column: x => x.TrafficLightId,
                        principalTable: "TrafficLights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Configurations_Users_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_LastUpdatedById",
                table: "Configurations",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_TrafficLightId",
                table: "Configurations",
                column: "TrafficLightId");

            migrationBuilder.CreateIndex(
                name: "IX_Intersections_LastUpdatedById",
                table: "Intersections",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficLights_IntersectionId",
                table: "TrafficLights",
                column: "IntersectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficLights_LastUpdatedById",
                table: "TrafficLights",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthIdentityId",
                table: "Users",
                column: "AuthIdentityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "TrafficLights");

            migrationBuilder.DropTable(
                name: "Intersections");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
