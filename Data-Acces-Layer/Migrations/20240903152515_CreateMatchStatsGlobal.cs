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
            migrationBuilder.DropPrimaryKey(
                name: "PK_summonerDataExpirations",
                table: "summonerDataExpirations");

            migrationBuilder.DropColumn(
                name: "HasExpired",
                table: "summonerDataExpirations");

            migrationBuilder.RenameTable(
                name: "summonerDataExpirations",
                newName: "SummonerDataExpiration");

            migrationBuilder.RenameColumn(
                name: "LastTimePlayed",
                table: "SummonerDataExpiration",
                newName: "NextTimeAllowed");

            migrationBuilder.AlterColumn<string>(
                name: "SummonerName",
                table: "SummonerDataExpiration",
                type: "NVARCHAR(35)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SummonerDataExpiration",
                table: "SummonerDataExpiration",
                column: "SummonerName");

            migrationBuilder.CreateTable(
                name: "LaneAverageMinionsGlobal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaneName = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    AverageMonstersKilled = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaneAverageMinionsGlobal", x => x.Id);
                });

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
                    TeamPosition = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "LaneAverageMinionsGlobal");

            migrationBuilder.DropTable(
                name: "MatchesStatisticsGlobal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SummonerDataExpiration",
                table: "SummonerDataExpiration");

            migrationBuilder.RenameTable(
                name: "SummonerDataExpiration",
                newName: "summonerDataExpirations");

            migrationBuilder.RenameColumn(
                name: "NextTimeAllowed",
                table: "summonerDataExpirations",
                newName: "LastTimePlayed");

            migrationBuilder.AlterColumn<string>(
                name: "SummonerName",
                table: "summonerDataExpirations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(35)",
                oldMaxLength: 35);

            migrationBuilder.AddColumn<bool>(
                name: "HasExpired",
                table: "summonerDataExpirations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_summonerDataExpirations",
                table: "summonerDataExpirations",
                column: "SummonerName");
        }
    }
}
