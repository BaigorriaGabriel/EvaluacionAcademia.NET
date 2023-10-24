﻿// <auto-generated />
using System;
using EvaluacionAcademia.NET.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EvaluacionAcademia.NET.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231024040626_CryptoWallet17")]
    partial class CryptoWallet17
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EvaluacionAcademia.NET.Entities.Account", b =>
                {
                    b.Property<int>("CodAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codAccount");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodAccount"), 1L, 1);

                    b.Property<int>("CodUser")
                        .HasColumnType("Int")
                        .HasColumnName("codUser");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("isActive");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type");

                    b.HasKey("CodAccount");

                    b.HasIndex("CodUser");

                    b.ToTable("Accounts", (string)null);

                    b.HasDiscriminator<string>("Type").HasValue("Account");
                });

            modelBuilder.Entity("EvaluacionAcademia.NET.Entities.Transaction", b =>
                {
                    b.Property<int>("CodTransaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codTransaction");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodTransaction"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("amount");

                    b.Property<int>("CodAccountSender")
                        .HasColumnType("Int")
                        .HasColumnName("codAccountSender");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("timestamp");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type");

                    b.HasKey("CodTransaction");

                    b.HasIndex("CodAccountSender");

                    b.ToTable("Transactions", (string)null);

                    b.HasDiscriminator<string>("Type").HasValue("Transaction");
                });

            modelBuilder.Entity("EvaluacionAcademia.NET.Entities.User", b =>
                {
                    b.Property<int>("CodUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codUser");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodUser"), 1L, 1);

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("dni");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("isActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("password");

                    b.HasKey("CodUser");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            CodUser = 1,
                            Dni = "44504788",
                            Email = "gabi.2912@hotmail.com",
                            IsActive = true,
                            Name = "Gabriel Baigorria",
                            Password = "1234"
                        });
                });

            modelBuilder.Entity("EvaluacionAcademia.NET.Entities.AccountCripto", b =>
                {
                    b.HasBaseType("EvaluacionAcademia.NET.Entities.Account");

                    b.Property<float>("BalanceBtc")
                        .HasColumnType("real")
                        .HasColumnName("balanceBtc");

                    b.Property<string>("DirectionUUID")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("directionUUID");

                    b.ToTable("Accounts");

                    b.HasDiscriminator().HasValue("Cripto");

                    b.HasData(
                        new
                        {
                            CodAccount = 2,
                            CodUser = 1,
                            IsActive = true,
                            Type = "Cripto",
                            BalanceBtc = 5f,
                            DirectionUUID = "asd123"
                        });
                });

            modelBuilder.Entity("EvaluacionAcademia.NET.Entities.AccountFiduciary", b =>
                {
                    b.HasBaseType("EvaluacionAcademia.NET.Entities.Account");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("accountNumber");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("alias");

                    b.Property<float>("BalancePeso")
                        .HasColumnType("real")
                        .HasColumnName("balancePeso");

                    b.Property<float>("BalanceUsd")
                        .HasColumnType("real")
                        .HasColumnName("balanceUsd");

                    b.Property<string>("CBU")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("CBU");

                    b.ToTable("Accounts");

                    b.HasDiscriminator().HasValue("Fiduciary");

                    b.HasData(
                        new
                        {
                            CodAccount = 1,
                            CodUser = 1,
                            IsActive = true,
                            Type = "Fiduciary",
                            AccountNumber = "123",
                            Alias = "gabriel.baigorria.cw",
                            BalancePeso = 250f,
                            BalanceUsd = 10f,
                            CBU = "111111"
                        });
                });

            modelBuilder.Entity("EvaluacionAcademia.NET.Entities.TransactionConversion", b =>
                {
                    b.HasBaseType("EvaluacionAcademia.NET.Entities.Transaction");

                    b.Property<string>("FromCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("fromCurrency");

                    b.Property<string>("ToCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("toCurrency");

                    b.ToTable("Transactions");

                    b.HasDiscriminator().HasValue("Conversion");
                });

            modelBuilder.Entity("EvaluacionAcademia.NET.Entities.TransactionTransfer", b =>
                {
                    b.HasBaseType("EvaluacionAcademia.NET.Entities.Transaction");

                    b.Property<int>("CodAccountReceiver")
                        .HasColumnType("Int")
                        .HasColumnName("codAccountReceiver");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("currency");

                    b.ToTable("Transactions");

                    b.HasDiscriminator().HasValue("Transfer");
                });

            modelBuilder.Entity("EvaluacionAcademia.NET.Entities.Account", b =>
                {
                    b.HasOne("EvaluacionAcademia.NET.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("CodUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EvaluacionAcademia.NET.Entities.Transaction", b =>
                {
                    b.HasOne("EvaluacionAcademia.NET.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("CodAccountSender")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}