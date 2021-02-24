﻿// <auto-generated />
using System;
using Kryptoteket.Bot.CosmosDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kryptoteket.Bot.Migrations
{
    [DbContext(typeof(KryptoteketContext))]
    [Migration("20210224124218_betlocked")]
    partial class betlocked
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Kryptoteket.Bot.Models.Bets.Bet", b =>
                {
                    b.Property<int>("BetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AddedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("Locked")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BetId");

                    b.HasIndex("ShortName")
                        .IsUnique()
                        .HasFilter("[ShortName] IS NOT NULL");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Bets.BetUser", b =>
                {
                    b.Property<decimal>("BetUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("BetUserId");

                    b.ToTable("BetUsers");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Bets.FinishedBetPlacement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BetId")
                        .HasColumnType("int");

                    b.Property<decimal>("BetUserId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("Place")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BetUserId");

                    b.ToTable("FinishedBetPlacements");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.PlacedBet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BetId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("BetPlaced")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("BetUserId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BetId");

                    b.HasIndex("BetUserId");

                    b.ToTable("PlacedBets");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Reflinks.RefExchange", b =>
                {
                    b.Property<int>("RefExchangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("EmojiId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RefExchangeId");

                    b.ToTable("RefExchanges");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Reflinks.RefLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RefExchangeId")
                        .HasColumnType("int");

                    b.Property<decimal>("RefUserId")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("RefExchangeId");

                    b.HasIndex("RefUserId");

                    b.ToTable("RefLinks");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Reflinks.RefUser", b =>
                {
                    b.Property<decimal>("RefUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RefUserId");

                    b.ToTable("RefUsers");
                });

            modelBuilder.Entity("RefExchangeRefUser", b =>
                {
                    b.Property<int>("RefExchangesRefExchangeId")
                        .HasColumnType("int");

                    b.Property<decimal>("RefUsersRefUserId")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("RefExchangesRefExchangeId", "RefUsersRefUserId");

                    b.HasIndex("RefUsersRefUserId");

                    b.ToTable("RefExchangeRefUsers");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Bets.FinishedBetPlacement", b =>
                {
                    b.HasOne("Kryptoteket.Bot.Models.Bets.BetUser", null)
                        .WithMany("Placements")
                        .HasForeignKey("BetUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.PlacedBet", b =>
                {
                    b.HasOne("Kryptoteket.Bot.Models.Bets.Bet", null)
                        .WithMany("PlacedBets")
                        .HasForeignKey("BetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kryptoteket.Bot.Models.Bets.BetUser", null)
                        .WithMany("PlacedBets")
                        .HasForeignKey("BetUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Reflinks.RefLink", b =>
                {
                    b.HasOne("Kryptoteket.Bot.Models.Reflinks.RefExchange", null)
                        .WithMany("Reflinks")
                        .HasForeignKey("RefExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kryptoteket.Bot.Models.Reflinks.RefUser", null)
                        .WithMany("Reflinks")
                        .HasForeignKey("RefUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RefExchangeRefUser", b =>
                {
                    b.HasOne("Kryptoteket.Bot.Models.Reflinks.RefExchange", null)
                        .WithMany()
                        .HasForeignKey("RefExchangesRefExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kryptoteket.Bot.Models.Reflinks.RefUser", null)
                        .WithMany()
                        .HasForeignKey("RefUsersRefUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Bets.Bet", b =>
                {
                    b.Navigation("PlacedBets");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Bets.BetUser", b =>
                {
                    b.Navigation("PlacedBets");

                    b.Navigation("Placements");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Reflinks.RefExchange", b =>
                {
                    b.Navigation("Reflinks");
                });

            modelBuilder.Entity("Kryptoteket.Bot.Models.Reflinks.RefUser", b =>
                {
                    b.Navigation("Reflinks");
                });
#pragma warning restore 612, 618
        }
    }
}
