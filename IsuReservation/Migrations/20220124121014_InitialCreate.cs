using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsuReservation.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContactTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("2e5c86eb-ec54-44ad-bac5-e6928203cf85"), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2240), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2241), false, "Contact Type 3" },
                    { new Guid("5aa58205-0b51-4ec8-9822-8e61c728817c"), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2214), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2215), false, "Contact Type 2" },
                    { new Guid("64cb848f-43d6-47a6-a05d-2141a2aabdd4"), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2186), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2188), false, "Contact Type 1" }
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Favorite", "Image", "IsDeleted", "Name", "Rating" },
                values: new object[,]
                {
                    { new Guid("5e1d7f3d-210c-44e3-8854-a642d956aa55"), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(1911), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(1937), "Description for Destination 1", false, "https://as01.epimg.net/diarioas/imagenes/2021/03/08/actualidad/1615216406_061390_1615221159_sumario_normal.jpg", false, "Destination 1", 5 },
                    { new Guid("cbe11d8e-424e-4776-a536-2e7f5e290041"), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2040), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2041), "Description for Destination 3", false, "https://viajes.nationalgeographic.com.es/medio/2019/04/30/istock-840449198_07c3ef3b_1245x842.jpg", false, "Destination 3", 1 },
                    { new Guid("f0d0aa79-399a-4283-bd2f-acbf3eb537dc"), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2013), new DateTime(2022, 1, 24, 7, 10, 14, 696, DateTimeKind.Local).AddTicks(2014), "Description for Destination 2", false, "https://viajes.nationalgeographic.com.es/medio/2019/04/30/istock-840449198_07c3ef3b_1245x842.jpg", false, "Destination 2", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ContactId",
                table: "Reservations",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DestinationId",
                table: "Reservations",
                column: "DestinationId");
            
            var sp = @"CREATE PROCEDURE [dbo].[CreateContact]
                     @Id uniqueidentifier,                      
                     @Name varchar(50),
                     @PhoneNumber varchar (50),
                     @BirthDate datetime2,
                     @ContactTypeId uniqueidentifier,
                     @IsDeleted bit,
                     @DateCreated datetime2,
                     @DateModified datetime2
                AS
                BEGIN
                    SET NOCOUNT ON;
                    Insert into Contacts([Id],[Name],[PhoneNumber],[BirthDate],[ContactTypeId],[IsDeleted],[DateCreated],[DateModified])
                    Values (@Id, @Name, @PhoneNumber, @BirthDate, @ContactTypeId, @IsDeleted, @DateCreated, @DateModified)
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "ContactTypes");
        }
    }
}
