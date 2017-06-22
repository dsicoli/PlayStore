using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PlayStore.Model;

namespace PlayStore.Migrations
{
    [DbContext(typeof(PlayStoreDBContext))]
    partial class PlayStoreDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlayStore.Model.Apps", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("AppBrand")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Genre")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("LastUpdate")
                        .IsUnique()
                        .HasName("IX_Apps");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("PlayStore.Model.Compatibilities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppId");

                    b.Property<string>("DeviceType");

                    b.HasKey("Id");

                    b.HasIndex("AppId")
                        .HasName("IX_Compatibilities_AppId");

                    b.ToTable("Compatibilities");
                });

            modelBuilder.Entity("PlayStore.Model.Devices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeviceUsed")
                        .IsRequired()
                        .HasColumnType("varchar(2500)");

                    b.Property<int>("DownloadId");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("PlayStore.Model.Downloads", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppId");

                    b.Property<byte[]>("Successful")
                        .HasColumnType("binary(1)");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("IX_Downloads_UserId");

                    b.ToTable("Downloads");
                });

            modelBuilder.Entity("PlayStore.Model.Prices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppId");

                    b.Property<string>("Currency")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("AppId")
                        .HasName("IX_Prices_AppId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("PlayStore.Model.Ratings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DownloadId");

                    b.Property<string>("IndividualRating")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DownloadId")
                        .HasName("IX_Ratings_DownloadId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("PlayStore.Model.Uploads", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<int>("AppId");

                    b.Property<bool>("Update");

                    b.Property<int>("UserAppId");

                    b.Property<int>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("UsersId")
                        .HasName("IX_Uploads_UsersId");

                    b.ToTable("Uploads");
                });

            modelBuilder.Entity("PlayStore.Model.UserApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppsId");

                    b.Property<int>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("AppsId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserApp");
                });

            modelBuilder.Entity("PlayStore.Model.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Birth")
                        .HasColumnType("datetime");

                    b.Property<string>("City")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SecondEmail");

                    b.Property<string>("Surname")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("IX_Users");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlayStore.Model.Compatibilities", b =>
                {
                    b.HasOne("PlayStore.Model.Apps", "App")
                        .WithMany("Compatibilities")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayStore.Model.Devices", b =>
                {
                    b.HasOne("PlayStore.Model.Downloads", "IdNavigation")
                        .WithOne("Devices")
                        .HasForeignKey("PlayStore.Model.Devices", "Id")
                        .HasConstraintName("FK_Devices_Downloads");
                });

            modelBuilder.Entity("PlayStore.Model.Downloads", b =>
                {
                    b.HasOne("PlayStore.Model.Users", "User")
                        .WithMany("Downloads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayStore.Model.Prices", b =>
                {
                    b.HasOne("PlayStore.Model.Apps", "App")
                        .WithMany("Prices")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayStore.Model.Ratings", b =>
                {
                    b.HasOne("PlayStore.Model.Downloads", "Download")
                        .WithMany("Ratings")
                        .HasForeignKey("DownloadId")
                        .HasConstraintName("FK_Ratings_Downloads");
                });

            modelBuilder.Entity("PlayStore.Model.Uploads", b =>
                {
                    b.HasOne("PlayStore.Model.Users", "Users")
                        .WithMany("Uploads")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayStore.Model.UserApp", b =>
                {
                    b.HasOne("PlayStore.Model.Apps", "Apps")
                        .WithMany("UserApp")
                        .HasForeignKey("AppsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlayStore.Model.Users", "Users")
                        .WithMany("UserApp")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
