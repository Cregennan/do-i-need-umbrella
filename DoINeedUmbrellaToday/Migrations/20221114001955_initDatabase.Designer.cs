﻿// <auto-generated />
using DoINeedUmbrellaToday;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoINeedUmbrellaToday.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20221114001955_initDatabase")]
    partial class initDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("DoINeedUmbrellaToday.Models.TelegramBotProgress", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastQuery")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TelegramBotProgresses");
                });

            modelBuilder.Entity("DoINeedUmbrellaToday.Models.TelegramChat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TelegramChats");
                });
#pragma warning restore 612, 618
        }
    }
}
