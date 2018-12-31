﻿// <auto-generated />
using System;
using Lee.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lee.Abp.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(LeeAbpDbContext))]
    partial class LeeAbpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("Lee.Abp.Core.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("CreateTime");

                    b.Property<string>("CreateUser")
                        .HasColumnName("CreateUser")
                        .HasMaxLength(20);

                    b.Property<string>("DeleteFlag")
                        .HasColumnName("DeleteFlag")
                        .HasMaxLength(1);

                    b.Property<int>("Enabled");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnName("ModifyTime");

                    b.Property<string>("ModifyUser")
                        .HasColumnName("ModifyUser")
                        .HasMaxLength(20);

                    b.Property<int>("TenantId");

                    b.Property<DateTime>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("Version")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("Lee_Role");
                });

            modelBuilder.Entity("Lee.Abp.Core.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("CreateTime");

                    b.Property<string>("CreateUser")
                        .HasColumnName("CreateUser")
                        .HasMaxLength(20);

                    b.Property<string>("DeleteFlag")
                        .HasColumnName("DeleteFlag")
                        .HasMaxLength(1);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnName("ModifyTime");

                    b.Property<string>("ModifyUser")
                        .HasColumnName("ModifyUser")
                        .HasMaxLength(20);

                    b.Property<int>("RoleId");

                    b.Property<int>("TenantId");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("Version")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("wcs_system_user_role");
                });

            modelBuilder.Entity("Lee.Abp.Core.Users.TestTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("CreateTime");

                    b.Property<string>("CreateUser")
                        .HasColumnName("CreateUser")
                        .HasMaxLength(20);

                    b.Property<string>("DeleteFlag")
                        .HasColumnName("DeleteFlag")
                        .HasMaxLength(1);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnName("ModifyTime");

                    b.Property<string>("ModifyUser")
                        .HasColumnName("ModifyUser")
                        .HasMaxLength(20);

                    b.Property<int>("TenantId");

                    b.Property<int>("Test");

                    b.Property<int>("Test1");

                    b.Property<DateTime>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("Version")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("TestTables");
                });

            modelBuilder.Entity("Lee.Abp.Core.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("CreateTime");

                    b.Property<string>("CreateUser")
                        .HasColumnName("CreateUser")
                        .HasMaxLength(20);

                    b.Property<string>("DeleteFlag")
                        .HasColumnName("DeleteFlag")
                        .HasMaxLength(1);

                    b.Property<int>("Level");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnName("ModifyTime");

                    b.Property<string>("ModifyUser")
                        .HasColumnName("ModifyUser")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .HasMaxLength(10);

                    b.Property<int>("TenantId");

                    b.Property<int>("Test");

                    b.Property<int>("Test1");

                    b.Property<DateTime>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("Version")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("Lee_User");
                });
#pragma warning restore 612, 618
        }
    }
}
