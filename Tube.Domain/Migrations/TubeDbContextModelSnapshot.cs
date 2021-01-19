﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tube.Data;

namespace Tube.Data.Migrations
{
    [DbContext(typeof(TubeDbContext))]
    partial class TubeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Tube.Data.Models.Channel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChannelPicUrl");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("TubeUserId");

                    b.HasKey("Id");

                    b.HasIndex("TubeUserId")
                        .IsUnique()
                        .HasFilter("[TubeUserId] IS NOT NULL");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("Tube.Data.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChannelPicUrl");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Text");

                    b.Property<string>("TubeUserName");

                    b.Property<string>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Tube.Data.Models.History", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("TubeUserId");

                    b.Property<string>("VideoName");

                    b.HasKey("Id");

                    b.HasIndex("TubeUserId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("Tube.Data.Models.Playlist", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("TubeUserId");

                    b.HasKey("Id");

                    b.HasIndex("TubeUserId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("Tube.Data.Models.TubeUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Tube.Data.Models.TubeUserChannel", b =>
                {
                    b.Property<string>("TubeUserId");

                    b.Property<string>("ChannelId");

                    b.HasKey("TubeUserId", "ChannelId");

                    b.HasIndex("ChannelId");

                    b.ToTable("TubeUserChannels");
                });

            modelBuilder.Entity("Tube.Data.Models.Video", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("ChannelId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name");

                    b.Property<string>("VideoUrl");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Tube.Data.Models.VideoPlaylist", b =>
                {
                    b.Property<string>("VideoId");

                    b.Property<string>("PlaylistId");

                    b.HasKey("VideoId", "PlaylistId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("VideoPlaylists");
                });

            modelBuilder.Entity("Tube.Data.Models.Vote", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommentId");

                    b.Property<string>("TubeUserName");

                    b.Property<string>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("VideoId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Tube.Data.Models.TubeUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Tube.Data.Models.TubeUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tube.Data.Models.TubeUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Tube.Data.Models.TubeUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tube.Data.Models.Channel", b =>
                {
                    b.HasOne("Tube.Data.Models.TubeUser", "TubeUser")
                        .WithOne("Channel")
                        .HasForeignKey("Tube.Data.Models.Channel", "TubeUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Tube.Data.Models.Comment", b =>
                {
                    b.HasOne("Tube.Data.Models.Video", "Video")
                        .WithMany("Comments")
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("Tube.Data.Models.History", b =>
                {
                    b.HasOne("Tube.Data.Models.TubeUser", "TubeUser")
                        .WithMany("History")
                        .HasForeignKey("TubeUserId");
                });

            modelBuilder.Entity("Tube.Data.Models.Playlist", b =>
                {
                    b.HasOne("Tube.Data.Models.TubeUser", "TubeUser")
                        .WithMany("Playlist")
                        .HasForeignKey("TubeUserId");
                });

            modelBuilder.Entity("Tube.Data.Models.TubeUserChannel", b =>
                {
                    b.HasOne("Tube.Data.Models.Channel", "Channel")
                        .WithMany("Subscribes")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tube.Data.Models.TubeUser", "TubeUser")
                        .WithMany("Subscriptions")
                        .HasForeignKey("TubeUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tube.Data.Models.Video", b =>
                {
                    b.HasOne("Tube.Data.Models.Channel", "Channel")
                        .WithMany("Videos")
                        .HasForeignKey("ChannelId");
                });

            modelBuilder.Entity("Tube.Data.Models.VideoPlaylist", b =>
                {
                    b.HasOne("Tube.Data.Models.Playlist", "Playlist")
                        .WithMany("Videos")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tube.Data.Models.Video", "Video")
                        .WithMany("Playlists")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tube.Data.Models.Vote", b =>
                {
                    b.HasOne("Tube.Data.Models.Comment", "Comment")
                        .WithMany("Likes")
                        .HasForeignKey("CommentId");

                    b.HasOne("Tube.Data.Models.Video", "Video")
                        .WithMany("Likes")
                        .HasForeignKey("VideoId");
                });
#pragma warning restore 612, 618
        }
    }
}