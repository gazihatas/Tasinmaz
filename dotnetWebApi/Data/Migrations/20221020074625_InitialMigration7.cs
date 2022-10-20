using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetWebApi.Data.Migrations
{
    public partial class InitialMigration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Il",
                columns: table => new
                {
                    IlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Il", x => x.IlId);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    logid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    durum = table.Column<bool>(type: "bit", nullable: false),
                    islemtipi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    acıklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    logIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.logid);
                });

            migrationBuilder.CreateTable(
                name: "Ilce",
                columns: table => new
                {
                    Ilceid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ilcename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilce", x => x.Ilceid);
                    table.ForeignKey(
                        name: "FK_Ilce_Il_IlId",
                        column: x => x.IlId,
                        principalTable: "Il",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mahalle",
                columns: table => new
                {
                    MahalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MahalleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mahalle", x => x.MahalleId);
                    table.ForeignKey(
                        name: "FK_Mahalle_Ilce_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilce",
                        principalColumn: "Ilceid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasinmaz",
                columns: table => new
                {
                    TasinmazId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Il = table.Column<int>(type: "int", nullable: false),
                    Ilce = table.Column<int>(type: "int", nullable: false),
                    MahalleId = table.Column<int>(type: "int", nullable: false),
                    Ada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parsel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kordinat = table.Column<int>(type: "int", nullable: false),
                    Nitelik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    coorX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coorY = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasinmaz", x => x.TasinmazId);
                    table.ForeignKey(
                        name: "FK_Tasinmaz_Mahalle_MahalleId",
                        column: x => x.MahalleId,
                        principalTable: "Mahalle",
                        principalColumn: "MahalleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ilce_IlId",
                table: "Ilce",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Mahalle_IlceId",
                table: "Mahalle",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmaz_MahalleId",
                table: "Tasinmaz",
                column: "MahalleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Tasinmaz");

            migrationBuilder.DropTable(
                name: "Mahalle");

            migrationBuilder.DropTable(
                name: "Ilce");

            migrationBuilder.DropTable(
                name: "Il");
        }
    }
}
