﻿// <auto-generated />
using System;
using DotNetApiCars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNetApiCars.Migrations
{
    [DbContext(typeof(CarContext))]
    [Migration("20241218205206_ReadyToReturn")]
    partial class ReadyToReturn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DotNetApiCars.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CarBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRented")
                        .HasColumnType("bit");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Localization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("DotNetApiCars.OfferDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriceDay")
                        .HasColumnType("int");

                    b.Property<int>("PriceInsurance")
                        .HasColumnType("int");

                    b.Property<DateTime>("WhenOfferWasMade")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("OffersDB");
                });

            modelBuilder.Entity("DotNetApiCars.RentHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Client_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsReadyToReturn")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReturned")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("DotNetApiCars.OfferDB", b =>
                {
                    b.HasOne("DotNetApiCars.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("DotNetApiCars.RentHistory", b =>
                {
                    b.HasOne("DotNetApiCars.OfferDB", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });
#pragma warning restore 612, 618
        }
    }
}
