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

            modelBuilder.Entity("BusinessEconomyManager.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.Business", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId")
                        .IsUnique();

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.BusinessExpenseTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<Guid>("BusinessPeriodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TransactionPaymentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessPeriodId");

                    b.HasIndex("SupplierId");

                    b.ToTable("BusinessExpenseTransactions");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.BusinessPeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AccountCashBalance")
                        .HasColumnType("float");

                    b.Property<double>("AccountCreditCardBalance")
                        .HasColumnType("float");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Closed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessPeriods");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.BusinessSaleTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<Guid>("BusinessPeriodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionPaymentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessPeriodId");

                    b.ToTable("BusinessSaleTransactions");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SupplierCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("SupplierCategoryId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.SupplierCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("SupplierCategories");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.Business", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.AppUser", "AppUser")
                        .WithOne("Business")
                        .HasForeignKey("BusinessEconomyManager.Models.Business", "AppUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.BusinessExpenseTransaction", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.BusinessPeriod", "BusinessPeriod")
                        .WithMany("BusinessExpenseTransactions")
                        .HasForeignKey("BusinessPeriodId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BusinessEconomyManager.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BusinessPeriod");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.BusinessPeriod", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.Business", "Business")
                        .WithMany("BusinessPeriods")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.BusinessSaleTransaction", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.BusinessPeriod", "BusinessPeriod")
                        .WithMany("BusinessSaleTransactions")
                        .HasForeignKey("BusinessPeriodId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BusinessPeriod");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.Supplier", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.Business", "Business")
                        .WithMany("Suppliers")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BusinessEconomyManager.Models.SupplierCategory", "SupplierCategory")
                        .WithMany()
                        .HasForeignKey("SupplierCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("SupplierCategory");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.SupplierCategory", b =>
                {
                    b.HasOne("BusinessEconomyManager.Models.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.AppUser", b =>
                {
                    b.Navigation("Business");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.Business", b =>
                {
                    b.Navigation("BusinessPeriods");

                    b.Navigation("Suppliers");
                });

            modelBuilder.Entity("BusinessEconomyManager.Models.BusinessPeriod", b =>
                {
                    b.Navigation("BusinessExpenseTransactions");

                    b.Navigation("BusinessSaleTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
