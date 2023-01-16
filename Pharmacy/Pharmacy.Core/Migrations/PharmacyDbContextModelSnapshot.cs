﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pharmacy.Core;

#nullable disable

namespace Pharmacy.Core.Migrations
{
    [DbContext(typeof(PharmacyDbContext))]
    partial class PharmacyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a5bef0e0-7646-4d98-ad00-5e17dac66c29",
                            ConcurrencyStamp = "7e896509-3179-423d-aa18-e7b304195332",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "8994c1d2-c79f-4108-a90f-0abfb4a3dcbb",
                            ConcurrencyStamp = "25062e71-2248-4b64-8f5f-f1a3b83ffa3c",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "691d8863-e296-4896-bd1e-61a0f9aff5c9",
                            ConcurrencyStamp = "bbb4aa8c-4821-4d88-bd15-2165de887142",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "8a9758e6-be23-4e74-940d-10397023641d",
                            RoleId = "a5bef0e0-7646-4d98-ad00-5e17dac66c29"
                        },
                        new
                        {
                            UserId = "8a9758e6-be23-4e74-940d-10397023641d",
                            RoleId = "8994c1d2-c79f-4108-a90f-0abfb4a3dcbb"
                        },
                        new
                        {
                            UserId = "824bce3a-ff7d-45ec-bf42-378dafbdce55",
                            RoleId = "8994c1d2-c79f-4108-a90f-0abfb4a3dcbb"
                        },
                        new
                        {
                            UserId = "629dfcde-5cb4-41e4-b60e-ec30ba357685",
                            RoleId = "691d8863-e296-4896-bd1e-61a0f9aff5c9"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Pharmacy.Core.Brend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brend");
                });

            modelBuilder.Entity("Pharmacy.Core.Catalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Catalog");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Лікарські засоби"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Краса та догляд"
                        });
                });

            modelBuilder.Entity("Pharmacy.Core.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CatalogId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Image = "C:\\Users\\Admin\\Documents\\GitHub\\Pharmacy_online\\Pharmacy\\Pharmacy.UI\\wwwroot\\img\\flu.jpg",
                            Name = "Застуда і грип"
                        },
                        new
                        {
                            Id = 2,
                            Image = "C:\\Users\\Admin\\Documents\\GitHub\\Pharmacy_online\\Pharmacy\\Pharmacy.UI\\wwwroot\\img\\heart.jpg",
                            Name = "Серцево-судинна система"
                        },
                        new
                        {
                            Id = 3,
                            Image = "C:\\Users\\Admin\\Documents\\GitHub\\Pharmacy_online\\Pharmacy\\Pharmacy.UI\\wwwroot\\img\\blood.jpg",
                            Name = "Кровотворення та кров"
                        });
                });

            modelBuilder.Entity("Pharmacy.Core.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Pharmacy.Core.Medicaments", b =>
                {
                    b.Property<int>("MedicamentsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicamentsId"), 1L, 1);

                    b.Property<int?>("BrendId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int?>("ProductLineId")
                        .HasColumnType("int");

                    b.Property<string>("ReleaseForm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicamentsId");

                    b.HasIndex("BrendId");

                    b.HasIndex("CountryId");

                    b.HasIndex("ProductLineId");

                    b.ToTable("Medicaments");

                    b.HasData(
                        new
                        {
                            MedicamentsId = 1,
                            Code = "4882",
                            Dosage = "",
                            Image = "C:\\Users\\HP\\Documents\\GitHub\\Pharmacy_online\\Pharmacy\\Pharmacy.UI\\wwwroot\\img\\sinupret.jpg",
                            Name = "Синупрет табл. в/о №50",
                            Price = 125.62f,
                            ReleaseForm = "таблетки для внутрішнього застосування"
                        },
                        new
                        {
                            MedicamentsId = 2,
                            Code = "2345",
                            Dosage = "12",
                            Image = "C:\\Users\\HP\\Documents\\GitHub\\Pharmacy_online\\Pharmacy\\Pharmacy.UI\\wwwroot\\img\\sinupret.jpg",
                            Name = "Синупрет",
                            Price = 89.75f,
                            ReleaseForm = "таблетки для внутрішнього застосування"
                        });
                });

            modelBuilder.Entity("Pharmacy.Core.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.Property<int?>("detailsId")
                        .HasColumnType("int");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("detailsId");

                    b.HasIndex("userId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Pharmacy.Core.OrderAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderAddress");
                });

            modelBuilder.Entity("Pharmacy.Core.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Payment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Total")
                        .HasColumnType("real");

                    b.Property<string>("TypeOfDelivery")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Pharmacy.Core.OrderItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MedicamentsId")
                        .HasColumnType("int");

                    b.Property<int>("OrderDetailsId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicamentsId");

                    b.HasIndex("OrderDetailsId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Pharmacy.Core.ProductLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductLine");
                });

            modelBuilder.Entity("Pharmacy.Core.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryId"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategory");

                    b.HasData(
                        new
                        {
                            SubCategoryId = 1,
                            Name = "Від кашлю"
                        });
                });

            modelBuilder.Entity("Pharmacy.Core.SubCategoryMedicaments", b =>
                {
                    b.Property<int>("MedicamentsId")
                        .HasColumnType("int");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("MedicamentsId", "SubCategoryId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("SubCategoryMedicaments");

                    b.HasData(
                        new
                        {
                            MedicamentsId = 1,
                            SubCategoryId = 1
                        },
                        new
                        {
                            MedicamentsId = 2,
                            SubCategoryId = 1
                        });
                });

            modelBuilder.Entity("Pharmacy.Core.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "8a9758e6-be23-4e74-940d-10397023641d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "90126efb-804b-494b-95e8-18b8db8cf893",
                            Email = "admin@pharmacy.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@PHARMACY.COM",
                            NormalizedUserName = "ADMIN@PHARMACY.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEKwkBBDIHUDQx3XIR0zQykQzxH6PN/EfjhOnXCMP5LG9E7slH+78Ni3IdtopsjgEEg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0254293d-c68c-4c1b-bdac-0a7ef0e202bc",
                            TwoFactorEnabled = false,
                            UserName = "admin@pharmacy.com"
                        },
                        new
                        {
                            Id = "824bce3a-ff7d-45ec-bf42-378dafbdce55",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9aa6d02f-c265-44b9-9ec8-873eb051dc07",
                            Email = "user@pharmacy.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@PHARMACY.COM",
                            NormalizedUserName = "USER@PHARMACY.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEL3e3otNwgUIbT1VedY2Ab9nMcOQp76duKuzFTTv5ui8Oh+hDfOJhJXkf8zfZ1MIyw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3f94a086-53d9-4fcd-ba13-aec9dbaa0b14",
                            TwoFactorEnabled = false,
                            UserName = "user@pharmacy.com"
                        },
                        new
                        {
                            Id = "629dfcde-5cb4-41e4-b60e-ec30ba357685",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "083c2889-a64c-40f0-a006-24a044751fec",
                            Email = "manager@pharmacy.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "MANAGER@PHARMACY.COM",
                            NormalizedUserName = "MANAGER@PHARMACY.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEJFJi1qzaWD4vXLvkUTBQ5EPg45rjnyg1QdX+zKBA/4dQgoHK9y26mOSPFn9kHWCEQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "05889514-9179-4d92-93c4-12fe8ad3f99d",
                            TwoFactorEnabled = false,
                            UserName = "manager@pharmacy.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Pharmacy.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Pharmacy.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Pharmacy.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pharmacy.Core.Category", b =>
                {
                    b.HasOne("Pharmacy.Core.Catalog", "Catalog")
                        .WithMany("Category")
                        .HasForeignKey("CatalogId");

                    b.Navigation("Catalog");
                });

            modelBuilder.Entity("Pharmacy.Core.Medicaments", b =>
                {
                    b.HasOne("Pharmacy.Core.Brend", "Brend")
                        .WithMany()
                        .HasForeignKey("BrendId");

                    b.HasOne("Pharmacy.Core.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Pharmacy.Core.ProductLine", "ProductLine")
                        .WithMany()
                        .HasForeignKey("ProductLineId");

                    b.Navigation("Brend");

                    b.Navigation("Country");

                    b.Navigation("ProductLine");
                });

            modelBuilder.Entity("Pharmacy.Core.Order", b =>
                {
                    b.HasOne("Pharmacy.Core.OrderDetails", "details")
                        .WithMany()
                        .HasForeignKey("detailsId");

                    b.HasOne("Pharmacy.Core.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");

                    b.Navigation("details");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Pharmacy.Core.OrderDetails", b =>
                {
                    b.HasOne("Pharmacy.Core.OrderAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Pharmacy.Core.OrderItems", b =>
                {
                    b.HasOne("Pharmacy.Core.Medicaments", "medicaments")
                        .WithMany()
                        .HasForeignKey("MedicamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.Core.OrderDetails", null)
                        .WithMany("orderItems")
                        .HasForeignKey("OrderDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("medicaments");
                });

            modelBuilder.Entity("Pharmacy.Core.SubCategory", b =>
                {
                    b.HasOne("Pharmacy.Core.Category", "Category")
                        .WithMany("SubCategory")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Pharmacy.Core.SubCategoryMedicaments", b =>
                {
                    b.HasOne("Pharmacy.Core.Medicaments", "Medicaments")
                        .WithMany("SubCategories")
                        .HasForeignKey("MedicamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.Core.SubCategory", "SubCategory")
                        .WithMany("Medicaments")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicaments");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("Pharmacy.Core.Catalog", b =>
                {
                    b.Navigation("Category");
                });

            modelBuilder.Entity("Pharmacy.Core.Category", b =>
                {
                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("Pharmacy.Core.Medicaments", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Pharmacy.Core.OrderDetails", b =>
                {
                    b.Navigation("orderItems");
                });

            modelBuilder.Entity("Pharmacy.Core.SubCategory", b =>
                {
                    b.Navigation("Medicaments");
                });
#pragma warning restore 612, 618
        }
    }
}
