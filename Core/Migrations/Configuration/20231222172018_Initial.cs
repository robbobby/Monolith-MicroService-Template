#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations.Configuration;

/// <inheritdoc />
public partial class Initial : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            "Units",
            table => new {
                Id = table.Column<Guid>("uuid", nullable: false),
                Name = table.Column<string>("text", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_Units", x => x.Id);
            });

        migrationBuilder.CreateTable(
            "Users",
            table => new {
                Id = table.Column<Guid>("uuid", nullable: false),
                FirstName = table.Column<string>("text", nullable: false),
                LastName = table.Column<string>("text", nullable: false),
                UserName = table.Column<string>("text", nullable: true),
                NormalizedUserName = table.Column<string>("text", nullable: true),
                Email = table.Column<string>("text", nullable: true),
                NormalizedEmail = table.Column<string>("text", nullable: true),
                EmailConfirmed = table.Column<bool>("boolean", nullable: false),
                PasswordHash = table.Column<string>("text", nullable: true),
                SecurityStamp = table.Column<string>("text", nullable: true),
                ConcurrencyStamp = table.Column<string>("text", nullable: true),
                PhoneNumber = table.Column<string>("text", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>("boolean", nullable: false),
                TwoFactorEnabled = table.Column<bool>("boolean", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>("timestamp with time zone", nullable: true),
                LockoutEnabled = table.Column<bool>("boolean", nullable: false),
                AccessFailedCount = table.Column<int>("integer", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            "UserUnits",
            table => new {
                UserId = table.Column<Guid>("uuid", nullable: false),
                UnitId = table.Column<Guid>("uuid", nullable: false),
                UsersId = table.Column<Guid>("uuid", nullable: false),
                UnitsId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_UserUnits", x => new { x.UserId, x.UnitId });
                table.ForeignKey(
                    "FK_UserUnits_Units_UnitsId",
                    x => x.UnitsId,
                    "Units",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_UserUnits_Users_UsersId",
                    x => x.UsersId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_UserUnits_UnitsId",
            "UserUnits",
            "UnitsId");

        migrationBuilder.CreateIndex(
            "IX_UserUnits_UsersId",
            "UserUnits",
            "UsersId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            "UserUnits");

        migrationBuilder.DropTable(
            "Units");

        migrationBuilder.DropTable(
            "Users");
    }
}