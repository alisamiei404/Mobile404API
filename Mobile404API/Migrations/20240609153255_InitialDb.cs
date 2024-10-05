using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mobile404API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Code = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ram = table.Column<int>(type: "int", nullable: false),
                    Hard = table.Column<int>(type: "int", nullable: false),
                    Camera = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Galleries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductToShops",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductToShops", x => new { x.ProductId, x.ShopId });
                    table.ForeignKey(
                        name: "FK_ProductToShops_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductToShops_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedAt", "ImageFileName", "IsActive", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(5251), "samsung.jpg", true, "سامسونگ", "samsung" },
                    { 2, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(5269), "nokia.jpg", true, "نوکیا", "nokia" },
                    { 3, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(5273), "xiaomi.jpg", true, "شیائومی", "xiaomi" },
                    { 4, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(5276), "apple.jpg", true, "اپل", "apple" },
                    { 5, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(5279), "huawei.jpg", true, "هوآوی", "huawei" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(5979), "admin@gmail.com", false, "ادمین", "81-DC-9B-DB-52-D0-4D-C2-00-36-DB-D8-31-3E-D0-55", "admin" },
                    { 2, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6025), "ali@gmail.com", false, "علی", "81-DC-9B-DB-52-D0-4D-C2-00-36-DB-D8-31-3E-D0-55", "seller" },
                    { 3, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6059), "tara@gmail.com", false, "تارا", "81-DC-9B-DB-52-D0-4D-C2-00-36-DB-D8-31-3E-D0-55", "client" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "Camera", "CreatedAt", "Description", "Hard", "Ram", "Title" },
                values: new object[,]
                {
                    { 1, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6201), "", 64, 4, "گوشی موبایل سامسونگ مدل Galaxy A14" },
                    { 2, 3, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6207), "", 256, 8, "گوشی موبایل شیائومی مدل Redmi 12" },
                    { 3, 5, 48, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6213), "", 128, 8, "گوشی موبایل هوآوی مدل nova Y71" },
                    { 4, 3, 108, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6217), "", 256, 8, "گوشی موبایل شیائومی مدل Redmi Note 13 4G" },
                    { 5, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6222), "", 64, 4, "گوشی موبایل سامسونگ مدل Galaxy A05" },
                    { 6, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6227), "", 64, 4, "گوشی موبایل سامسونگ مدل Galaxy M13" },
                    { 7, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6232), "", 128, 4, "گوشی موبایل سامسونگ مدل Galaxy A15" },
                    { 8, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6240), "", 128, 6, "گوشی موبایل سامسونگ مدل Galaxy A05s" },
                    { 9, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6245), "", 64, 4, "گوشی موبایل سامسونگ مدل Galaxy A23 " },
                    { 10, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6250), "", 128, 6, "گوشی موبایل سامسونگ مدل Galaxy A25" },
                    { 11, 1, 48, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6256), "", 128, 8, "گوشی موبایل سامسونگ مدل Galaxy A34 5G " },
                    { 12, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6261), "", 128, 8, "گوشی موبایل سامسونگ مدل Galaxy A35" },
                    { 13, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6267), "", 128, 8, "گوشی موبایل سامسونگ مدل Galaxy A54 5G" },
                    { 14, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6272), "", 256, 8, "گوشی موبایل سامسونگ مدل Galaxy A55" },
                    { 15, 1, 12, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6277), "", 256, 8, "گوشی موبایل سامسونگ مدل Galaxy S21 FE 5G" },
                    { 16, 1, 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6283), "", 256, 8, "گوشی موبایل سامسونگ مدل Galaxy S23 FE" },
                    { 17, 1, 200, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6288), "", 256, 12, "گوشی موبایل سامسونگ مدل Galaxy S23 Ultra" },
                    { 18, 1, 200, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6294), "", 512, 12, "گوشی موبایل سامسونگ مدل Galaxy S24 Ultra" },
                    { 19, 3, 108, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6299), "", 256, 8, "گوشی موبایل شیائومی مدل Redmi Note 12S" },
                    { 20, 3, 108, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6304), "", 256, 8, "گوشی موبایل شیائومی مدل Redmi Note 13 5G" },
                    { 21, 3, 108, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6310), "", 256, 12, "گوشی موبایل شیائومی مدل Redmi Note 12 Pro 5G" },
                    { 22, 3, 64, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6315), "", 256, 12, "گوشی موبایل شیائومی مدل Redmi Note 12 Turbo 5G" },
                    { 23, 3, 64, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6320), "", 256, 12, "گوشی موبایل شیائومی مدل Poco F5 5G" }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "CreatedAt", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(5622), "موبایل404", 1 },
                    { 2, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(5628), "علی شاپ", 2 }
                });

            migrationBuilder.InsertData(
                table: "Galleries",
                columns: new[] { "Id", "CreatedAt", "ImageFileName", "ProductId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6388), "samsung-a14-1.jpg", 1 },
                    { 2, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6392), "samsung-a14-2.jpg", 1 },
                    { 3, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6395), "huawei-nova-y71-1.jpg", 3 },
                    { 4, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6397), "huawei-nova-y71-2.jpg", 3 },
                    { 5, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6400), "huawei-nova-y71-3.jpg", 3 },
                    { 6, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6402), "xiaomi-redmi-note-13-1.jpg", 4 },
                    { 7, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6404), "Galaxy-A05-1.jpg", 5 },
                    { 8, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6406), "Galaxy-A05-2.jpg", 5 },
                    { 9, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6409), "Galaxy-M13-1.jpg", 6 },
                    { 10, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6411), "Galaxy-M13-2.jpg", 6 },
                    { 11, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6413), "Galaxy-M13-3.jpg", 6 },
                    { 12, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6418), "Galaxy-A15-1.jpg", 7 },
                    { 13, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6420), "Galaxy-A15-2.jpg", 7 },
                    { 14, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6422), "Galaxy-A15-3.jpg", 7 },
                    { 15, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6424), "Galaxy-A05s-1.jpg", 8 },
                    { 16, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6427), "Galaxy-A05s-2.jpg", 8 },
                    { 17, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6429), "Galaxy-A05s-3.jpg", 8 },
                    { 18, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6431), "Galaxy-A23-1.jpg", 9 },
                    { 19, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6433), "Galaxy-A23-2.jpg", 9 },
                    { 20, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6435), "Galaxy-A23-3.jpg", 9 },
                    { 21, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6438), "Galaxy-A25-1.jpg", 10 },
                    { 22, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6440), "Galaxy-A25-2.jpg", 10 },
                    { 23, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6442), "Galaxy-A25-3.jpg", 10 },
                    { 24, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6444), "Galaxy-A34-1.jpg", 11 },
                    { 25, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6446), "Galaxy-A34-2.jpg", 11 },
                    { 26, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6448), "Galaxy-A35-1.jpg", 12 },
                    { 27, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6450), "Galaxy-A35-2.jpg", 12 },
                    { 28, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6453), "Galaxy-A54-1.jpg", 13 },
                    { 29, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6455), "Galaxy-A54-2.jpg", 13 },
                    { 30, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6456), "Galaxy-A55-1.jpg", 14 },
                    { 31, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6458), "Galaxy-A55-2.jpg", 14 },
                    { 32, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6462), "Galaxy-A55-3.jpg", 14 },
                    { 33, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6465), "Galaxy-S21-1.jpg", 15 },
                    { 34, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6467), "Galaxy-S23-1.jpg", 16 },
                    { 35, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6470), "Galaxy-S23-2.jpg", 16 },
                    { 36, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6475), "Galaxy-S23-Ultra-1.jpg", 17 },
                    { 37, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6477), "Galaxy-S24-Ultra-1.jpg", 18 },
                    { 38, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6479), "Galaxy-S24-Ultra-2.jpg", 18 },
                    { 39, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6481), "Galaxy-S24-Ultra-3.jpg", 18 },
                    { 40, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6484), "Redmi-Note-12S-1.jpg", 19 },
                    { 41, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6486), "Redmi-Note-12S-2.jpg", 19 },
                    { 42, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6489), "Redmi-Note-13-1.jpg", 20 },
                    { 43, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6491), "Redmi-Note-13-2.jpg", 20 },
                    { 44, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6494), "Redmi-Note-13-3.jpg", 20 },
                    { 45, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6496), "Redmi-Note-12-1.jpg", 21 },
                    { 46, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6498), "Redmi-Note-12-Turbo-1.jpg", 22 },
                    { 47, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6500), "Redmi-Note-12-Turbo-2.jpg", 22 },
                    { 48, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6502), "Redmi-Note-12-Turbo-3.jpg", 22 },
                    { 49, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6504), "Poco-F5-1.jpg", 23 },
                    { 50, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6506), "Poco-F5-2.jpg", 23 },
                    { 51, new DateTime(2024, 6, 9, 19, 2, 55, 209, DateTimeKind.Local).AddTicks(6509), "Poco-F5-3.jpg", 23 }
                });

            migrationBuilder.InsertData(
                table: "ProductToShops",
                columns: new[] { "ProductId", "ShopId", "Price" },
                values: new object[,]
                {
                    { 1, 1, 6000000 },
                    { 2, 1, 6900000 },
                    { 2, 2, 6950000 },
                    { 3, 1, 6200000 },
                    { 4, 1, 8840000 },
                    { 5, 1, 8740000 },
                    { 6, 1, 5699000 },
                    { 7, 1, 6079000 },
                    { 8, 1, 6470000 },
                    { 9, 1, 7399000 },
                    { 10, 1, 9549000 },
                    { 10, 2, 9549000 },
                    { 11, 1, 12999000 },
                    { 12, 1, 15287000 },
                    { 13, 1, 16990000 },
                    { 14, 1, 20980000 },
                    { 15, 1, 21150000 },
                    { 16, 1, 25850000 },
                    { 17, 1, 57950000 },
                    { 19, 1, 8530000 },
                    { 20, 1, 11540000 },
                    { 20, 2, 11440000 },
                    { 21, 1, 12600000 },
                    { 22, 1, 18799000 },
                    { 23, 1, 17999000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Slug",
                table: "Brands",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_ProductId",
                table: "Galleries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShopId",
                table: "Orders",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResets_Email",
                table: "PasswordResets",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductToShops_ShopId",
                table: "ProductToShops",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_UserId",
                table: "Shops",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PasswordResets");

            migrationBuilder.DropTable(
                name: "ProductToShops");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
