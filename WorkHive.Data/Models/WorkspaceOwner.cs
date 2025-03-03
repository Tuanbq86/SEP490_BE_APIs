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

    public string IdentityName { get; set; }

    public string IdentityNumber { get; set; }

    public DateTime? Dob { get; set; }

    public string Sex { get; set; }

    public string Nationality { get; set; }

    public string PlaceOfOrigin { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string GoogleMapUrl { get; set; }

    public string Status { get; set; }

    public string PlaceOfResidence { get; set; }

    public DateTime? IdentityExpiredDate { get; set; }

    public DateTime? IdentityCreatedDate { get; set; }

    public string IdentityFile { get; set; }

    public string LicenseName { get; set; }

    public string LicenseNumber { get; set; }

    public string LicenseAddress { get; set; }

    public decimal? CharterCapital { get; set; }

    public string LicenseFile { get; set; }

    public string Facebook { get; set; }

    public string Instagram { get; set; }

    public string Tiktok { get; set; }

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();

    public virtual ICollection<Beverage> Beverages { get; set; } = new List<Beverage>();

    public virtual ICollection<OwnerWallet> OwnerWallets { get; set; } = new List<OwnerWallet>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();
}