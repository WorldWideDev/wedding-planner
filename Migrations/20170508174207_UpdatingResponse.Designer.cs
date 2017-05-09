using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WeddingPlanner.Models;

namespace WeddingPlanner.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20170508174207_UpdatingResponse")]
    partial class UpdatingResponse
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("WeddingPlanner.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<int>("WeddingId");

                    b.Property<int>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("WeddingId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("WeddingPlanner.Models.Response", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsGoing");

                    b.Property<int>("UserId");

                    b.Property<int?>("WeddingId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WeddingId");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("WeddingPlanner.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WeddingPlanner.Models.Wedding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("HostId");

                    b.Property<string>("WedderOne");

                    b.Property<string>("WedderTwo");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.ToTable("Weddings");
                });

            modelBuilder.Entity("WeddingPlanner.Models.Address", b =>
                {
                    b.HasOne("WeddingPlanner.Models.Wedding", "Wedding")
                        .WithOne("EventLocation")
                        .HasForeignKey("WeddingPlanner.Models.Address", "WeddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeddingPlanner.Models.Response", b =>
                {
                    b.HasOne("WeddingPlanner.Models.User", "User")
                        .WithMany("Responses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WeddingPlanner.Models.Wedding", "Wedding")
                        .WithMany("Responses")
                        .HasForeignKey("WeddingId");
                });

            modelBuilder.Entity("WeddingPlanner.Models.Wedding", b =>
                {
                    b.HasOne("WeddingPlanner.Models.User", "Host")
                        .WithMany()
                        .HasForeignKey("HostId");
                });
        }
    }
}
