﻿// <auto-generated />
using System;
using BusinessEconomyManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessEconomyManager.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessEconomyManager.Models.ServiceSuppliedType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserBusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserBusinessId");

                    b.ToTable("ServiceSuppliedTypes");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ServiceSuppliedTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserBusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ServiceSuppliedTypeId");

                    b.HasIndex("UserBusinessId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.SupplierOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("PaidAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("PaymentTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserBusinessPeriodId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserBusinessPeriodId");

                    b.ToTable("SupplierOperations");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GivenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasFilter("[EmailAddress] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.UserBusiness", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserBusinesses");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.UserBusinessPeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserBusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserBusinessId");

                    b.ToTable("UserBusinessPeriods");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.ServiceSuppliedType", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.UserBusiness", "UserBusiness")
                        .WithMany("ServiceSuppliedTypes")
                        .HasForeignKey("UserBusinessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("UserBusiness");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.Supplier", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.ServiceSuppliedType", "ServiceSuppliedType")
                        .WithMany("Suppliers")
                        .HasForeignKey("ServiceSuppliedTypeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BusinessEconomyManager.Models.UserBusiness", "UserBusiness")
                        .WithMany("Suppliers")
                        .HasForeignKey("UserBusinessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ServiceSuppliedType");

                    b.Navigation("UserBusiness");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.SupplierOperation", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.Supplier", "Supplier")
                        .WithMany("SupplierOperations")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BusinessEconomyManager.Models.UserBusinessPeriod", "UserBusinessPeriod")
                        .WithMany("SupplierOperations")
                        .HasForeignKey("UserBusinessPeriodId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Supplier");

                    b.Navigation("UserBusinessPeriod");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.UserBusiness", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.User", "User")
                        .WithMany("UserBusinesses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.UserBusinessPeriod", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.UserBusiness", "UserBusiness")
                        .WithMany("UserBusinessPeriods")
                        .HasForeignKey("UserBusinessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("UserBusiness");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.ServiceSuppliedType", b =>
                {
                    b.Navigation("Suppliers");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.Supplier", b =>
                {
                    b.Navigation("SupplierOperations");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.User", b =>
                {
                    b.Navigation("UserBusinesses");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.UserBusiness", b =>
                {
                    b.Navigation("ServiceSuppliedTypes");

                    b.Navigation("Suppliers");

                    b.Navigation("UserBusinessPeriods");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.UserBusinessPeriod", b =>
                {
                    b.Navigation("SupplierOperations");
                });
#pragma warning restore 612, 618
        }
    }
}
