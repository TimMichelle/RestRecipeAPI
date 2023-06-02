﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RestRecipeApp.Persistence;

#nullable disable

namespace RestRecipeApp.Persistence.Migrations
{
    [DbContext(typeof(RecipesDbContext))]
    [Migration("20230602210230_AddShoppingList")]
    partial class AddShoppingList
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RestRecipeApp.Persistence.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IngredientId"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.Property<string>("UnitOfMeasurement")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IngredientId");

                    b.HasIndex("ProductId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RestRecipeApp.Persistence.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RestRecipeApp.Persistence.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RecipeId"));

                    b.Property<int>("CookingTime")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalPersons")
                        .HasColumnType("integer");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RestRecipeApp.Persistence.Models.RecipeStep", b =>
                {
                    b.Property<int>("RecipeStepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RecipeStepId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.Property<int>("StepNumber")
                        .HasColumnType("integer");

                    b.HasKey("RecipeStepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");
                });

            modelBuilder.Entity("RestRecipeApp.Persistence.Models.ShoppingList", b =>
                {
                    b.Property<int>("ShoppingListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ShoppingListId"));

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.HasKey("ShoppingListId");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("RestRecipeApp.Persistence.Models.ShoppingListItem", b =>
                {
                    b.Property<int>("ShoppingListItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ShoppingListItemId"));

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsBought")
                        .HasColumnType("boolean");

                    b.Property<int>("ShoppingListId")
                        .HasColumnType("integer");

                    b.HasKey("ShoppingListItemId");

                    b.ToTable("ShoppingListItems");
                });

            modelBuilder.Entity("RestRecipeApp.Persistence.Models.Ingredient", b =>
                {
                    b.HasOne("RestRecipeApp.Persistence.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestRecipeApp.Persistence.Models.Recipe", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RestRecipeApp.Persistence.Models.RecipeStep", b =>
                {
                    b.HasOne("RestRecipeApp.Persistence.Models.Recipe", null)
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestRecipeApp.Persistence.Models.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Steps");
                });
#pragma warning restore 612, 618
        }
    }
}
