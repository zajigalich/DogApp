using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DogApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dogs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    tail_length = table.Column<short>(type: "smallint", nullable: false),
                    weight = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dogs", x => x.id);
                    table.CheckConstraint("CK_Dogs_TailLength_GreaterThanZero", "[tail_length] > 0");
                    table.CheckConstraint("CK_Dogs_Weight_GreaterThanZero", "[weight] > 0");
                });

            migrationBuilder.InsertData(
                table: "dogs",
                columns: new[] { "id", "color", "name", "tail_length", "weight" },
                values: new object[,]
                {
                    { new Guid("4107f4c3-0f2a-4e41-869f-5d1884ac2fc5"), "orange", "Babangida", (short)5, (short)12 },
                    { new Guid("639c89ce-c711-4aad-9b80-bf7d9afec10b"), "black", "IceCube", (short)22, (short)38 },
                    { new Guid("841146ef-4fc7-4d72-b8eb-9e72be4f5a25"), "black", "Snoop", (short)22, (short)38 },
                    { new Guid("9315a1af-1df0-4fc4-ab84-03388d14b2a2"), "brown", "50Cent", (short)14, (short)44 },
                    { new Guid("a1c9b960-0dfa-4d6b-825a-e7f9cc464ed9"), "black & brown", "Kanye West", (short)10, (short)30 },
                    { new Guid("a2d8578f-02d4-4c3f-80b9-6c9763767dcf"), "black & white", "Jessy", (short)7, (short)14 },
                    { new Guid("adb36a6d-4d2a-4a6e-9c9e-df460915f5b1"), "red & amber", "Neo", (short)22, (short)32 },
                    { new Guid("b75c7548-aaed-4a0a-a614-9eddb7d1bc2b"), "black", "Phife Dawg", (short)4, (short)9 },
                    { new Guid("e6bc34c9-40ef-45b7-b1ff-9ba9ecbd1f34"), "white", "Eminem", (short)14, (short)13 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_dogs_name",
                table: "dogs",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dogs");
        }
    }
}
