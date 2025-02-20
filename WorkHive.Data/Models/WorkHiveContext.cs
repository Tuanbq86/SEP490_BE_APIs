﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WorkHive.Data.Models;

public partial class WorkHiveContext : DbContext
{
    public WorkHiveContext(DbContextOptions<WorkHiveContext> options)
        : base(options)
    {
    }
    public WorkHiveContext()
    {
        
    }
    public virtual DbSet<Amenity> Amenities { get; set; }

    public virtual DbSet<Beverage> Beverages { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingAmenity> BookingAmenities { get; set; }

    public virtual DbSet<BookingBeverage> BookingBeverages { get; set; }

    public virtual DbSet<CustomerWallet> CustomerWallets { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<FacilityWorkspace> FacilityWorkspaces { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<ImageFeedback> ImageFeedbacks { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<OwnerTransactionHistory> OwnerTransactionHistories { get; set; }

    public virtual DbSet<OwnerWallet> OwnerWallets { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserTransactionHistory> UserTransactionHistories { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    public virtual DbSet<Workspace> Workspaces { get; set; }

    public virtual DbSet<WorkspaceImage> WorkspaceImages { get; set; }

    public virtual DbSet<WorkspacePolicy> WorkspacePolicies { get; set; }

    public virtual DbSet<WorkspacePrice> WorkspacePrices { get; set; }

    public virtual DbSet<WorkspaceRating> WorkspaceRatings { get; set; }

    public virtual DbSet<WorkspaceRatingImage> WorkspaceRatingImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Amenity__3214EC272EF53DAD");

            entity.ToTable("Amenity");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(255)
                .HasColumnName("img_url");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Owner).WithMany(p => p.Amenities)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKAmenity971690");
        });

        modelBuilder.Entity<Beverage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Beverage__3214EC277183037D");

            entity.ToTable("Beverage");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(255)
                .HasColumnName("img_url");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Owner).WithMany(p => p.Beverages)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBeverage498721");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC276928EF73");

            entity.ToTable("Booking");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WorkspaceId).HasColumnName("WorkspaceID");

            entity.HasOne(d => d.Payment).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking795232");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PromotionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking49948");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking176015");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking333556");
        });

        modelBuilder.Entity<BookingAmenity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking___3214EC27031A9990");

            entity.ToTable("Booking_Amenity");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AmenityId).HasColumnName("AmenityID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Amenity).WithMany(p => p.BookingAmenities)
                .HasForeignKey(d => d.AmenityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking_Am697085");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingAmenities)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking_Am538428");
        });

        modelBuilder.Entity<BookingBeverage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking___3214EC27D0118FD4");

            entity.ToTable("Booking_Beverage");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BeverageId).HasColumnName("BeverageID");
            entity.Property(e => e.BookingWorkspaceId).HasColumnName("Booking_WorkspaceID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Beverage).WithMany(p => p.BookingBeverages)
                .HasForeignKey(d => d.BeverageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking_Be174662");

            entity.HasOne(d => d.BookingWorkspace).WithMany(p => p.BookingBeverages)
                .HasForeignKey(d => d.BookingWorkspaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking_Be931755");
        });

        modelBuilder.Entity<CustomerWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC27AE304362");

            entity.ToTable("Customer_Wallet");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WalletId).HasColumnName("WalletID");

            entity.HasOne(d => d.User).WithMany(p => p.CustomerWallets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCustomer_W978919");

            entity.HasOne(d => d.Wallet).WithMany(p => p.CustomerWallets)
                .HasForeignKey(d => d.WalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCustomer_W786976");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__facility__3214EC2777A441BE");

            entity.ToTable("facility");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<FacilityWorkspace>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.FacilityId, e.WorkspaceId }).HasName("PK__facility__D879E35A2213F722");

            entity.ToTable("facility_Workspace");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.FacilityId).HasColumnName("facilityID");
            entity.Property(e => e.WorkspaceId).HasColumnName("WorkspaceID");

            entity.HasOne(d => d.Facility).WithMany(p => p.FacilityWorkspaces)
                .HasForeignKey(d => d.FacilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKfacility_W828201");

            entity.HasOne(d => d.Workspace).WithMany(p => p.FacilityWorkspaces)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKfacility_W348891");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC27C48211C8");

            entity.ToTable("Feedback");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFeedback374627");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Image__3214EC270A4DFE5D");

            entity.ToTable("Image");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(255)
                .HasColumnName("img_url");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ImageFeedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Image_Fe__3214EC27216E4549");

            entity.ToTable("Image_Feedback");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Feedback).WithMany(p => p.ImageFeedbacks)
                .HasForeignKey(d => d.FeedbackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKImage_Feed513527");

            entity.HasOne(d => d.Image).WithMany(p => p.ImageFeedbacks)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKImage_Feed509918");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC2778663B11");

            entity.ToTable("Notification");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Owner__3214EC27269265EB");

            entity.ToTable("Owner");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(50)
                .HasColumnName("business_name");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.GoogleMapUrl)
                .HasColumnType("text")
                .HasColumnName("google_map_url");
            entity.Property(e => e.License)
                .HasMaxLength(255)
                .HasColumnName("license");
            entity.Property(e => e.OwnerIdentityCard)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("owner_identity_card");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(50)
                .HasColumnName("owner_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.SocialUrl)
                .HasColumnType("text")
                .HasColumnName("social_url");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<OwnerTransactionHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Owner_Tr__3214EC27CE9AEB70");

            entity.ToTable("Owner_Transaction_History");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OwnerWalletId).HasColumnName("Owner_WalletID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TransactionHistoryId).HasColumnName("Transaction_HistoryID");

            entity.HasOne(d => d.OwnerWallet).WithMany(p => p.OwnerTransactionHistories)
                .HasForeignKey(d => d.OwnerWalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKOwner_Tran177184");

            entity.HasOne(d => d.TransactionHistory).WithMany(p => p.OwnerTransactionHistories)
                .HasForeignKey(d => d.TransactionHistoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKOwner_Tran330923");
        });

        modelBuilder.Entity<OwnerWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Owner_Wa__3214EC27E8EBCF7E");

            entity.ToTable("Owner_Wallet");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.WalletId).HasColumnName("WalletID");

            entity.HasOne(d => d.Owner).WithMany(p => p.OwnerWallets)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKOwner_Wall571813");

            entity.HasOne(d => d.Wallet).WithMany(p => p.OwnerWallets)
                .HasForeignKey(d => d.WalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKOwner_Wall535444");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC27DE4E35B8");

            entity.ToTable("Payment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Policy__3214EC27BE7E85C4");

            entity.ToTable("Policy");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Price__3214EC274C021CC9");

            entity.ToTable("Price");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Price1)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC27344989CD");

            entity.ToTable("Promotion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Owner).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPromotion450926");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rating__3214EC2733010221");

            entity.ToTable("Rating");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRating217340");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC2742483158");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC27DBF4D33D");

            entity.ToTable("Transaction_History");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC277373EB40");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .HasColumnName("avatar");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.Location)
                .HasColumnType("text")
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser349791");
        });

        modelBuilder.Entity<UserTransactionHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User_Tra__3214EC27D3073CEA");

            entity.ToTable("User_Transaction_History");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CustomerWalletId).HasColumnName("Customer_WalletID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TransactionHistoryId).HasColumnName("Transaction_HistoryID");

            entity.HasOne(d => d.CustomerWallet).WithMany(p => p.UserTransactionHistories)
                .HasForeignKey(d => d.CustomerWalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser_Trans9893");

            entity.HasOne(d => d.TransactionHistory).WithMany(p => p.UserTransactionHistories)
                .HasForeignKey(d => d.TransactionHistoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser_Trans431856");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Wallet__3214EC276301F168");

            entity.ToTable("Wallet");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("balance");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Workspace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workspac__3214EC2705D1355C");

            entity.ToTable("Workspace");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CleanTime).HasColumnName("clean_time");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Owner).WithMany(p => p.Workspaces)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace597278");
        });

        modelBuilder.Entity<WorkspaceImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workspac__3214EC278C4130DC");

            entity.ToTable("Workspace_Image");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.Status)
                .HasColumnType("datetime")
                .HasColumnName("status");
            entity.Property(e => e.WorkspaceId).HasColumnName("WorkspaceID");

            entity.HasOne(d => d.Image).WithMany(p => p.WorkspaceImages)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_260933");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspaceImages)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_795085");
        });

        modelBuilder.Entity<WorkspacePolicy>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.WorkspaceId, e.PolicyId }).HasName("PK__Workspac__7BBE89456DC6AA49");

            entity.ToTable("Workspace_Policy");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.WorkspaceId).HasColumnName("WorkspaceID");
            entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

            entity.HasOne(d => d.Policy).WithMany(p => p.WorkspacePolicies)
                .HasForeignKey(d => d.PolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_55574");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspacePolicies)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_370091");
        });

        modelBuilder.Entity<WorkspacePrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workspac__3214EC27CBF7457A");

            entity.ToTable("Workspace_Price");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.WorkspaceId).HasColumnName("WorkspaceID");

            entity.HasOne(d => d.Price).WithMany(p => p.WorkspacePrices)
                .HasForeignKey(d => d.PriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_493014");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspacePrices)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_173913");
        });

        modelBuilder.Entity<WorkspaceRating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workspac__3214EC27F6EC3DF8");

            entity.ToTable("Workspace_Rating");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RatingId).HasColumnName("RatingID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.WorkspaceId).HasColumnName("WorkspaceID");

            entity.HasOne(d => d.Rating).WithMany(p => p.WorkspaceRatings)
                .HasForeignKey(d => d.RatingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_38200");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspaceRatings)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_802387");
        });

        modelBuilder.Entity<WorkspaceRatingImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workspac__3214EC27AC663122");

            entity.ToTable("Workspace_Rating_Image");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.WorkspaceRatingId).HasColumnName("Workspace_RatingID");

            entity.HasOne(d => d.Image).WithMany(p => p.WorkspaceRatingImages)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_235900");

            entity.HasOne(d => d.WorkspaceRating).WithMany(p => p.WorkspaceRatingImages)
                .HasForeignKey(d => d.WorkspaceRatingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkspace_818274");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}