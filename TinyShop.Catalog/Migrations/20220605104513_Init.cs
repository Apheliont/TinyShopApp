using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TinyShop.Catalog.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    caption = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    uri_size_s = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    uri_size_m = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    uri_size_l = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_main = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    language_code = table.Column<string>(type: "text", nullable: false),
                    language_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_languages", x => x.language_code);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    original_language_language_code = table.Column<string>(type: "text", nullable: false),
                    category_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    image_id = table.Column<int>(type: "integer", nullable: true),
                    parent_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_categories_parent_node_id",
                        column: x => x.parent_id,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_categories_images_image_id",
                        column: x => x.image_id,
                        principalTable: "images",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_categories_languages_original_language_temp_id",
                        column: x => x.original_language_language_code,
                        principalTable: "languages",
                        principalColumn: "language_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "category_filters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    original_language_language_code = table.Column<string>(type: "text", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    measurement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_filters", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_filters_languages_original_language_temp_id1",
                        column: x => x.original_language_language_code,
                        principalTable: "languages",
                        principalColumn: "language_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    country_code = table.Column<string>(type: "text", nullable: false),
                    country_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    currency_sign = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    language_code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.country_code);
                    table.ForeignKey(
                        name: "fk_countries_languages_language_temp_id2",
                        column: x => x.language_code,
                        principalTable: "languages",
                        principalColumn: "language_code");
                });

            migrationBuilder.CreateTable(
                name: "category_translations",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    language_code = table.Column<string>(type: "text", nullable: false),
                    category_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_translations", x => new { x.category_id, x.language_code });
                    table.ForeignKey(
                        name: "fk_category_translations_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    original_language_language_code = table.Column<string>(type: "text", nullable: true),
                    product_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    details = table.Column<JsonDocument>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_products_languages_original_language_language_code",
                        column: x => x.original_language_language_code,
                        principalTable: "languages",
                        principalColumn: "language_code");
                });

            migrationBuilder.CreateTable(
                name: "category_category_filter",
                columns: table => new
                {
                    categories_id = table.Column<int>(type: "integer", nullable: false),
                    category_filters_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_category_filter", x => new { x.categories_id, x.category_filters_id });
                    table.ForeignKey(
                        name: "fk_category_category_filter_categories_categories_id",
                        column: x => x.categories_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_category_category_filter_category_filters_category_filters_",
                        column: x => x.category_filters_id,
                        principalTable: "category_filters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "category_filter_translations",
                columns: table => new
                {
                    category_filter_id = table.Column<int>(type: "integer", nullable: false),
                    language_code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    measurement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_filter_translations", x => new { x.category_filter_id, x.language_code });
                    table.ForeignKey(
                        name: "fk_category_filter_translations_category_filters_category_filt",
                        column: x => x.category_filter_id,
                        principalTable: "category_filters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_translations",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    language_code = table.Column<string>(type: "text", nullable: false),
                    product_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_translations", x => new { x.product_id, x.language_code });
                    table.ForeignKey(
                        name: "fk_product_translations_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products_images",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products_images", x => new { x.image_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_products_images_images_image_id",
                        column: x => x.image_id,
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_products_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_categories_image_id",
                table: "categories",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "ix_categories_original_language_language_code",
                table: "categories",
                column: "original_language_language_code");

            migrationBuilder.CreateIndex(
                name: "ix_categories_parent_id",
                table: "categories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_category_filter_category_filters_id",
                table: "category_category_filter",
                column: "category_filters_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_filters_original_language_language_code",
                table: "category_filters",
                column: "original_language_language_code");

            migrationBuilder.CreateIndex(
                name: "ix_countries_language_code",
                table: "countries",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_original_language_language_code",
                table: "products",
                column: "original_language_language_code");

            migrationBuilder.CreateIndex(
                name: "ix_products_images_image_id_product_id",
                table: "products_images",
                columns: new[] { "image_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_images_product_id",
                table: "products_images",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category_category_filter");

            migrationBuilder.DropTable(
                name: "category_filter_translations");

            migrationBuilder.DropTable(
                name: "category_translations");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "product_translations");

            migrationBuilder.DropTable(
                name: "products_images");

            migrationBuilder.DropTable(
                name: "category_filters");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "languages");
        }
    }
}
