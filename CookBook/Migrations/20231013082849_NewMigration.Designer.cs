﻿// <auto-generated />
using System;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CookBook.Migrations
{
    [DbContext(typeof(RecipeContext))]
    [Migration("20231013082849_NewMigration")]
    partial class NewMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CookBook.Models.IngredientMeasurement", b =>
                {
                    b.Property<int>("IngredientMeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientMeasurementId"), 1L, 1);

                    b.Property<int>("IngredientsId")
                        .HasColumnType("int");

                    b.Property<int>("MeasurementIngredientUnit")
                        .HasColumnType("int");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("IngredientMeasurementId");

                    b.HasIndex("IngredientsId");

                    b.HasIndex("RecipeId");

                    b.ToTable("IngredientMeasurementDs");
                });

            modelBuilder.Entity("CookBook.Models.Ingredients", b =>
                {
                    b.Property<int>("IngredientsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientsId"), 1L, 1);

                    b.Property<string>("IngredientName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IngredientsId");

                    b.ToTable("IngredientsDs");
                });

            modelBuilder.Entity("CookBook.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecipeId");

                    b.ToTable("RecipeDs");
                });

            modelBuilder.Entity("CookBook.Models.Recipe_Note", b =>
                {
                    b.Property<int>("Recipe_NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Recipe_NoteId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Use_RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Recipe_NoteId");

                    b.HasIndex("Use_RecipeId");

                    b.ToTable("Recipe_Note");
                });

            modelBuilder.Entity("CookBook.Models.Use_Recipe", b =>
                {
                    b.Property<int>("Use_RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Use_RecipeId"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Use_RecipeId");

                    b.ToTable("Use_RecipeDs");
                });

            modelBuilder.Entity("CookBook.Models.IngredientMeasurement", b =>
                {
                    b.HasOne("CookBook.Models.Ingredients", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CookBook.Models.Recipe", null)
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("CookBook.Models.Recipe_Note", b =>
                {
                    b.HasOne("CookBook.Models.Use_Recipe", null)
                        .WithMany("Notes")
                        .HasForeignKey("Use_RecipeId");
                });

            modelBuilder.Entity("CookBook.Models.Recipe", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("CookBook.Models.Use_Recipe", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
