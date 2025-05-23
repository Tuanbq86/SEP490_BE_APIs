﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class WorkspaceOwner
{
    public int Id { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Sex { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string GoogleMapUrl { get; set; }

    public string Status { get; set; }

    public string LicenseName { get; set; }

    public string LicenseNumber { get; set; }

    public string LicenseAddress { get; set; }

    public decimal? CharterCapital { get; set; }

    public string LicenseFile { get; set; }

    public string Facebook { get; set; }

    public string Instagram { get; set; }

    public string Tiktok { get; set; }

    public string PhoneStatus { get; set; }

    public int? IsBan { get; set; }

    public string Avatar { get; set; }

    public string OwnerName { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();

    public virtual ICollection<Beverage> Beverages { get; set; } = new List<Beverage>();

    public virtual ICollection<OwnerNotification> OwnerNotifications { get; set; } = new List<OwnerNotification>();

    public virtual ICollection<OwnerPasswordResetToken> OwnerPasswordResetTokens { get; set; } = new List<OwnerPasswordResetToken>();

    public virtual ICollection<OwnerResponseFeedback> OwnerResponseFeedbacks { get; set; } = new List<OwnerResponseFeedback>();

    public virtual ICollection<OwnerVerifyRequest> OwnerVerifyRequests { get; set; } = new List<OwnerVerifyRequest>();

    public virtual ICollection<OwnerWallet> OwnerWallets { get; set; } = new List<OwnerWallet>();

    public virtual ICollection<OwnerWithdrawalRequest> OwnerWithdrawalRequests { get; set; } = new List<OwnerWithdrawalRequest>();

    public virtual ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();
}