using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lee.Abp.EntityFrameworkCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lee_Role",
                columns: table => new
                {
                    TenantId = table.Column<int>(nullable: false),
                    Version = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateUser = table.Column<string>(maxLength: 20, nullable: true),
                    ModifyUser = table.Column<string>(maxLength: 20, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<string>(maxLength: 1, nullable: true),
                    Enabled = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lee_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lee_User",
                columns: table => new
                {
                    TenantId = table.Column<int>(nullable: false),
                    Version = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateUser = table.Column<string>(maxLength: 20, nullable: true),
                    ModifyUser = table.Column<string>(maxLength: 20, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<string>(maxLength: 1, nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Test = table.Column<int>(nullable: false),
                    Test1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lee_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TestTables",
                columns: table => new
                {
                    TenantId = table.Column<int>(nullable: false),
                    Version = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateUser = table.Column<string>(maxLength: 20, nullable: true),
                    ModifyUser = table.Column<string>(maxLength: 20, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<string>(maxLength: 1, nullable: true),
                    Test = table.Column<int>(nullable: false),
                    Test1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTables", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "wcs_system_user_role",
                columns: table => new
                {
                    TenantId = table.Column<int>(nullable: false),
                    Version = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateUser = table.Column<string>(maxLength: 20, nullable: true),
                    ModifyUser = table.Column<string>(maxLength: 20, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<string>(maxLength: 1, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wcs_system_user_role", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lee_Role");

            migrationBuilder.DropTable(
                name: "Lee_User");

            migrationBuilder.DropTable(
                name: "TestTables");

            migrationBuilder.DropTable(
                name: "wcs_system_user_role");
        }
    }
}
