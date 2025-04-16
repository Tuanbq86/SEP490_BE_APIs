﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class OwnerVerifyRequest
{
    public int Id { get; set; }

    public int OwnerId { get; set; }

    public int? UserId { get; set; }

    public string Message { get; set; }

    public string Status { get; set; }

    public string GoogleMapUrl { get; set; }

    public string LicenseName { get; set; }

    public string LicenseNumber { get; set; }

    public string LicenseAddress { get; set; }

    public decimal? CharterCapital { get; set; }

    public string LicenseFile { get; set; }

    public string OwnerName { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public string Facebook { get; set; }

    public string Instagram { get; set; }

    public string Tiktok { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual WorkspaceOwner Owner { get; set; }

    public virtual User User { get; set; }
}