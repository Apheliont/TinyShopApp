// <auto-generated />
using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TinyShop.Catalog;

#nullable disable

namespace TinyShop.Catalog.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryCategoryFilter", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("integer")
                        .HasColumnName("categories_id");

                    b.Property<int>("CategoryFiltersId")
                        .HasColumnType("integer")
                        .HasColumnName("category_filters_id");

                    b.HasKey("CategoriesId", "CategoryFiltersId")
                        .HasName("pk_category_category_filter");

                    b.HasIndex("CategoryFiltersId")
                        .HasDatabaseName("ix_category_category_filter_category_filters_id");

                    b.ToTable("category_category_filter", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("category_name");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int?>("ImageId")
                        .HasColumnType("integer")
                        .HasColumnName("image_id");

                    b.Property<string>("OriginalLanguageLanguageCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("original_language_language_code");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.HasIndex("ImageId")
                        .HasDatabaseName("ix_categories_image_id");

                    b.HasIndex("OriginalLanguageLanguageCode")
                        .HasDatabaseName("ix_categories_original_language_language_code");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("ix_categories_parent_id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.CategoryFilter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("index");

                    b.Property<string>("Measurement")
                        .HasColumnType("text")
                        .HasColumnName("measurement");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("OriginalLanguageLanguageCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("original_language_language_code");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_category_filters");

                    b.HasIndex("OriginalLanguageLanguageCode")
                        .HasDatabaseName("ix_category_filters_original_language_language_code");

                    b.ToTable("category_filters", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.CategoryFilterTranslation", b =>
                {
                    b.Property<int>("CategoryFilterId")
                        .HasColumnType("integer")
                        .HasColumnName("category_filter_id");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("text")
                        .HasColumnName("language_code");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Measurement")
                        .HasColumnType("text")
                        .HasColumnName("measurement");

                    b.HasKey("CategoryFilterId", "LanguageCode")
                        .HasName("pk_category_filter_translations");

                    b.ToTable("category_filter_translations", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.CategoryTranslation", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("text")
                        .HasColumnName("language_code");

                    b.Property<string>("CategoryName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("category_name");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("CategoryId", "LanguageCode")
                        .HasName("pk_category_translations");

                    b.ToTable("category_translations", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Country", b =>
                {
                    b.Property<string>("CountryCode")
                        .HasColumnType("text")
                        .HasColumnName("country_code");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("country_name");

                    b.Property<string>("CurrencySign")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)")
                        .HasColumnName("currency_sign");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("text")
                        .HasColumnName("language_code");

                    b.HasKey("CountryCode")
                        .HasName("pk_countries");

                    b.HasIndex("LanguageCode")
                        .HasDatabaseName("ix_countries_language_code");

                    b.ToTable("countries", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("caption");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("UriSizeL")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("uri_size_l");

                    b.Property<string>("UriSizeM")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("uri_size_m");

                    b.Property<string>("UriSizeS")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("uri_size_s");

                    b.HasKey("Id")
                        .HasName("pk_images");

                    b.ToTable("images", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Language", b =>
                {
                    b.Property<string>("LanguageCode")
                        .HasColumnType("text")
                        .HasColumnName("language_code");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("language_name");

                    b.HasKey("LanguageCode")
                        .HasName("pk_languages");

                    b.ToTable("languages", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<JsonDocument>("Details")
                        .HasColumnType("jsonb")
                        .HasColumnName("details");

                    b.Property<string>("OriginalLanguageLanguageCode")
                        .HasColumnType("text")
                        .HasColumnName("original_language_language_code");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("product_name");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_products_category_id");

                    b.HasIndex("OriginalLanguageLanguageCode")
                        .HasDatabaseName("ix_products_original_language_language_code");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.ProductsImages", b =>
                {
                    b.Property<int>("ImageId")
                        .HasColumnType("integer")
                        .HasColumnName("image_id");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.HasKey("ImageId", "ProductId")
                        .HasName("pk_products_images");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_products_images_product_id");

                    b.HasIndex("ImageId", "ProductId")
                        .IsUnique()
                        .HasDatabaseName("ix_products_images_image_id_product_id");

                    b.ToTable("products_images", (string)null);
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.ProductTranslation", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("text")
                        .HasColumnName("language_code");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ProductName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("product_name");

                    b.HasKey("ProductId", "LanguageCode")
                        .HasName("pk_product_translations");

                    b.ToTable("product_translations", (string)null);
                });

            modelBuilder.Entity("CategoryCategoryFilter", b =>
                {
                    b.HasOne("TinyShop.Catalog.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_category_filter_categories_categories_id");

                    b.HasOne("TinyShop.Catalog.Entities.CategoryFilter", null)
                        .WithMany()
                        .HasForeignKey("CategoryFiltersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_category_filter_category_filters_category_filters_");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Category", b =>
                {
                    b.HasOne("TinyShop.Catalog.Entities.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .HasConstraintName("fk_categories_images_image_id");

                    b.HasOne("TinyShop.Catalog.Entities.Language", "OriginalLanguage")
                        .WithMany()
                        .HasForeignKey("OriginalLanguageLanguageCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_categories_languages_original_language_temp_id");

                    b.HasOne("TinyShop.Catalog.Entities.Category", "ParentNode")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("fk_categories_categories_parent_node_id");

                    b.Navigation("Image");

                    b.Navigation("OriginalLanguage");

                    b.Navigation("ParentNode");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.CategoryFilter", b =>
                {
                    b.HasOne("TinyShop.Catalog.Entities.Language", "OriginalLanguage")
                        .WithMany()
                        .HasForeignKey("OriginalLanguageLanguageCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_filters_languages_original_language_temp_id1");

                    b.Navigation("OriginalLanguage");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.CategoryFilterTranslation", b =>
                {
                    b.HasOne("TinyShop.Catalog.Entities.CategoryFilter", null)
                        .WithMany("CategoryFilterTranslations")
                        .HasForeignKey("CategoryFilterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_filter_translations_category_filters_category_filt");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.CategoryTranslation", b =>
                {
                    b.HasOne("TinyShop.Catalog.Entities.Category", null)
                        .WithMany("CategoryTranslations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_translations_categories_category_id");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Country", b =>
                {
                    b.HasOne("TinyShop.Catalog.Entities.Language", null)
                        .WithMany("Countries")
                        .HasForeignKey("LanguageCode")
                        .HasConstraintName("fk_countries_languages_language_temp_id2");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Product", b =>
                {
                    b.HasOne("TinyShop.Catalog.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_products_categories_category_id");

                    b.HasOne("TinyShop.Catalog.Entities.Language", "OriginalLanguage")
                        .WithMany()
                        .HasForeignKey("OriginalLanguageLanguageCode")
                        .HasConstraintName("fk_products_languages_original_language_language_code");

                    b.Navigation("Category");

                    b.Navigation("OriginalLanguage");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.ProductsImages", b =>
                {
                    b.HasOne("TinyShop.Catalog.Entities.Image", "Image")
                        .WithMany("ProductsImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_products_images_images_image_id");

                    b.HasOne("TinyShop.Catalog.Entities.Product", "Product")
                        .WithMany("ProductsImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_products_images_products_product_id");

                    b.Navigation("Image");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.ProductTranslation", b =>
                {
                    b.HasOne("TinyShop.Catalog.Entities.Product", null)
                        .WithMany("ProductTranslations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_translations_products_product_id");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Category", b =>
                {
                    b.Navigation("CategoryTranslations");

                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.CategoryFilter", b =>
                {
                    b.Navigation("CategoryFilterTranslations");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Image", b =>
                {
                    b.Navigation("ProductsImages");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Language", b =>
                {
                    b.Navigation("Countries");
                });

            modelBuilder.Entity("TinyShop.Catalog.Entities.Product", b =>
                {
                    b.Navigation("ProductTranslations");

                    b.Navigation("ProductsImages");
                });
#pragma warning restore 612, 618
        }
    }
}
