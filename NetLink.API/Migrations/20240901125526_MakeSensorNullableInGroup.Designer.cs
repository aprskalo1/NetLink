﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetLink.API.Data;

#nullable disable

namespace NetLink.API.Migrations
{
    [DbContext(typeof(NetLinkDbContext))]
    [Migration("20240901125526_MakeSensorNullableInGroup")]
    partial class MakeSensorNullableInGroup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NetLink.API.Models.Developer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DevToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("NetLink.API.Models.DeveloperUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DeveloperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EndUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("EndUserId");

                    b.ToTable("DeveloperUsers");
                });

            modelBuilder.Entity("NetLink.API.Models.EndUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EndUsers");
                });

            modelBuilder.Entity("NetLink.API.Models.EndUserSensor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("SensorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.HasIndex("SensorId");

                    b.ToTable("EndUserSensors");
                });

            modelBuilder.Entity("NetLink.API.Models.EndUserSensorGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("SensorGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.HasIndex("SensorGroupId");

                    b.ToTable("EndUserSensorGroups");
                });

            modelBuilder.Entity("NetLink.API.Models.RecordedValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RecordedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SensorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("RecordedValues");
                });

            modelBuilder.Entity("NetLink.API.Models.Sensor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeasurementUnit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("NetLink.API.Models.SensorGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SensorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("SensorGroups");
                });

            modelBuilder.Entity("NetLink.API.Models.DeveloperUser", b =>
                {
                    b.HasOne("NetLink.API.Models.Developer", "Developer")
                        .WithMany("DeveloperUsers")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetLink.API.Models.EndUser", "EndUser")
                        .WithMany("DeveloperUsers")
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("EndUser");
                });

            modelBuilder.Entity("NetLink.API.Models.EndUserSensor", b =>
                {
                    b.HasOne("NetLink.API.Models.EndUser", "EndUser")
                        .WithMany("EndUserSensors")
                        .HasForeignKey("EndUserId");

                    b.HasOne("NetLink.API.Models.Sensor", "Sensor")
                        .WithMany("EndUserSensors")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndUser");

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("NetLink.API.Models.EndUserSensorGroup", b =>
                {
                    b.HasOne("NetLink.API.Models.EndUser", "EndUser")
                        .WithMany("EndUserSensorGroups")
                        .HasForeignKey("EndUserId");

                    b.HasOne("NetLink.API.Models.SensorGroup", "SensorGroup")
                        .WithMany("EndUserSensorGroups")
                        .HasForeignKey("SensorGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndUser");

                    b.Navigation("SensorGroup");
                });

            modelBuilder.Entity("NetLink.API.Models.RecordedValue", b =>
                {
                    b.HasOne("NetLink.API.Models.Sensor", "Sensor")
                        .WithMany()
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("NetLink.API.Models.SensorGroup", b =>
                {
                    b.HasOne("NetLink.API.Models.Sensor", "Sensor")
                        .WithMany()
                        .HasForeignKey("SensorId");

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("NetLink.API.Models.Developer", b =>
                {
                    b.Navigation("DeveloperUsers");
                });

            modelBuilder.Entity("NetLink.API.Models.EndUser", b =>
                {
                    b.Navigation("DeveloperUsers");

                    b.Navigation("EndUserSensorGroups");

                    b.Navigation("EndUserSensors");
                });

            modelBuilder.Entity("NetLink.API.Models.Sensor", b =>
                {
                    b.Navigation("EndUserSensors");
                });

            modelBuilder.Entity("NetLink.API.Models.SensorGroup", b =>
                {
                    b.Navigation("EndUserSensorGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
