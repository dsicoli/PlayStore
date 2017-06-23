using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PlayStore.Model
{
    public partial class PlayStoreDBContext : DbContext
    {
        public virtual DbSet<App> Apps { get; set; }
        public virtual DbSet<Compatibility> Compatibilities { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Download> Downloads { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<Upload> Uploads { get; set; }
        public virtual DbSet<UserApp> UserApp { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=NewPlayStoreDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<App>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.LastUpdate)
                    .HasName("IX_Apps");

                // entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AppBrand).HasColumnType("varchar(250)");

                entity.Property(e => e.Genre).HasColumnType("varchar(250)");

                entity.Property(e => e.LastUpdate)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Name).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Compatibility>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.HasIndex(e => e.AppId)
                    .HasName("IX_Compatibilities_AppId");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.Compatibilities)
                    .HasForeignKey(d => d.AppId);
            });

            modelBuilder.Entity<Devices>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.DeviceUsed)
                    .IsRequired()
                    .HasColumnType("varchar(2500)");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Devices)
                    .HasForeignKey<Devices>(d => d.Id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Devices_Downloads");
            });

            modelBuilder.Entity<Download>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Downloads_UserId");

                entity.Property(e => e.Successful).HasColumnType("binary(1)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Downloads)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.AppId)
                    .HasName("IX_Prices_AppId");

                entity.Property(e => e.Currency).HasColumnType("varchar(50)");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.AppId);
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.DownloadId)
                    .HasName("IX_Ratings_DownloadId");

                entity.Property(e => e.IndividualRating).HasColumnType("varchar(max)");

                entity.HasOne(d => d.Download)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.DownloadId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Ratings_Downloads");
            });

            modelBuilder.Entity<Upload>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.UsersId)
                    .HasName("IX_Uploads_UsersId");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Uploads)
                    .HasForeignKey(d => d.UsersId);
            });

            modelBuilder.Entity<UserApp>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // entity.HasIndex(e => e.AppsId)
                //     .HasName("IX_UserApp_AppsId");

                // entity.HasIndex(e => e.UsersId)
                //     .HasName("IX_UserApp_UsersId");

                entity.HasOne(d => d.Apps)
                    .WithMany(p => p.UserApp)
                    .HasForeignKey(d => d.AppsId);

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.UserApp)
                    .HasForeignKey(d => d.UsersId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Email)
                    .HasName("IX_Users")
                    .IsUnique();

                entity.Property(e => e.Birth).HasColumnType("datetime");

                entity.Property(e => e.City).HasColumnType("varchar(50)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasColumnType("varchar(50)");

                entity.Property(e => e.Surname).HasColumnType("varchar(50)");
            });
        }
    }
}