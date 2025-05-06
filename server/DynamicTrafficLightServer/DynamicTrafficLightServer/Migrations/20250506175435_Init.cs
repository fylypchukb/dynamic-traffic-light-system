using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicTrafficLightServer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AuthIdentityId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityChangeLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    ChangedById = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityChangeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityChangeLogs_Users_ChangedById",
                        column: x => x.ChangedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Intersections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intersections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intersections_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
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
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_TrafficLights_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
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
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_Configurations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Configurations_Users_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrafficSwitchLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrafficLightId = table.Column<int>(type: "int", nullable: false),
                    VehicleCount = table.Column<int>(type: "int", nullable: false),
                    GreenLightDurationSeconds = table.Column<int>(type: "int", nullable: false),
                    InitById = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficSwitchLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrafficSwitchLogs_TrafficLights_TrafficLightId",
                        column: x => x.TrafficLightId,
                        principalTable: "TrafficLights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrafficSwitchLogs_Users_InitById",
                        column: x => x.InitById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthIdentityId", "Name" },
                values: new object[] { 1, "system", "System" });

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_CreatedById",
                table: "Configurations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_LastUpdatedById",
                table: "Configurations",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_TrafficLightId",
                table: "Configurations",
                column: "TrafficLightId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityChangeLogs_ChangedById",
                table: "EntityChangeLogs",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_Intersections_CreatedById",
                table: "Intersections",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Intersections_LastUpdatedById",
                table: "Intersections",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficLights_CreatedById",
                table: "TrafficLights",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficLights_IntersectionId",
                table: "TrafficLights",
                column: "IntersectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficLights_LastUpdatedById",
                table: "TrafficLights",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficSwitchLogs_InitById",
                table: "TrafficSwitchLogs",
                column: "InitById");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficSwitchLogs_TrafficLightId",
                table: "TrafficSwitchLogs",
                column: "TrafficLightId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthIdentityId",
                table: "Users",
                column: "AuthIdentityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "EntityChangeLogs");

            migrationBuilder.DropTable(
                name: "TrafficSwitchLogs");

            migrationBuilder.DropTable(
                name: "TrafficLights");

            migrationBuilder.DropTable(
                name: "Intersections");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
