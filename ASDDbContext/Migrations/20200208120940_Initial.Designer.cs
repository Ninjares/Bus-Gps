﻿// <auto-generated />
using ASPDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASDDbContext.Migrations
{
    [DbContext(typeof(TransportDbContext))]
    [Migration("20200208120940_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASPDbContext.Models.Bus", b =>
                {
                    b.Property<int>("BusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BusLoginHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.HasKey("BusId");

                    b.HasIndex("LineId");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("ASPDbContext.Models.BusLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusLines");
                });

            modelBuilder.Entity("ASPDbContext.Models.BusStop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PointId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PointId")
                        .IsUnique();

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("ASPDbContext.Models.LinePoint", b =>
                {
                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<int>("PointId")
                        .HasColumnType("int");

                    b.Property<int>("RowPosition")
                        .HasColumnType("int");

                    b.HasKey("LineId", "PointId", "RowPosition");

                    b.HasIndex("PointId");

                    b.ToTable("LinePoints");
                });

            modelBuilder.Entity("ASPDbContext.Models.LineStop", b =>
                {
                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<int>("StopId")
                        .HasColumnType("int");

                    b.HasKey("LineId", "StopId");

                    b.HasIndex("StopId");

                    b.ToTable("LineStops");
                });

            modelBuilder.Entity("ASPDbContext.Models.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("ASPDbContext.Models.Bus", b =>
                {
                    b.HasOne("ASPDbContext.Models.BusLine", "Line")
                        .WithMany("Buses")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ASPDbContext.Models.BusStop", b =>
                {
                    b.HasOne("ASPDbContext.Models.Point", "Point")
                        .WithOne()
                        .HasForeignKey("ASPDbContext.Models.BusStop", "PointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ASPDbContext.Models.LinePoint", b =>
                {
                    b.HasOne("ASPDbContext.Models.BusLine", "Line")
                        .WithMany("Route")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASPDbContext.Models.Point", "Point")
                        .WithMany("LinePoints")
                        .HasForeignKey("PointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ASPDbContext.Models.LineStop", b =>
                {
                    b.HasOne("ASPDbContext.Models.BusLine", "Line")
                        .WithMany("Stops")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASPDbContext.Models.BusStop", "BusStop")
                        .WithMany("LineStops")
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}