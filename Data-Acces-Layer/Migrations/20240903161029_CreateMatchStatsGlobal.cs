using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Acces_Layer.Migrations
{
    /// <inheritdoc />
    public partial class CreateMatchStatsGlobal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "MatchesStatisticsGlobal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChampionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinionsFirst10Minutes = table.Column<int>(type: "int", nullable: false),
                    ControlWardsPlaced = table.Column<int>(type: "int", nullable: false),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    GameLength = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamPosition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCS = table.Column<int>(type: "int", nullable: false),
                    CSperMinute = table.Column<float>(type: "real", nullable: false),
                    Win = table.Column<bool>(type: "bit", nullable: false),
                    VisionScore = table.Column<int>(type: "int", nullable: false),
                    TotalDamageDealtToChampions = table.Column<int>(type: "int", nullable: false),
                    WardTakedowns = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchesStatisticsGlobal", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchesStatisticsGlobal");

        }
    }
}
