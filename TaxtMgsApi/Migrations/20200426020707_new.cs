using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxtMgsApi.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    LastLogin = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "ContactRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxApplications",
                columns: table => new
                {
                    TaxAppId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bvn = table.Column<string>(nullable: true),
                    Tin = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<string>(nullable: true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    CurrentPositon = table.Column<string>(nullable: true),
                    StaffId = table.Column<string>(nullable: true),
                    CompanyPhoneNo = table.Column<string>(nullable: true),
                    CurrentSalary = table.Column<string>(nullable: true),
                    CompanyWebsite = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxApplications", x => x.TaxAppId);
                });

            migrationBuilder.CreateTable(
                name: "TaxRegistrations",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxAppId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRegistrations", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_TaxRegistrations_TaxApplications_TaxAppId",
                        column: x => x.TaxAppId,
                        principalTable: "TaxApplications",
                        principalColumn: "TaxAppId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRecords",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    ExpiredDate = table.Column<string>(nullable: true),
                    Cv2 = table.Column<string>(nullable: true),
                    Pin = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    TaxAppId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRecords", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentRecords_TaxRegistrations_UserId",
                        column: x => x.UserId,
                        principalTable: "TaxRegistrations",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_UserId",
                table: "PaymentRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxRegistrations_TaxAppId",
                table: "TaxRegistrations",
                column: "TaxAppId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ContactRecords");

            migrationBuilder.DropTable(
                name: "PaymentRecords");

            migrationBuilder.DropTable(
                name: "TaxRegistrations");

            migrationBuilder.DropTable(
                name: "TaxApplications");
        }
    }
}
