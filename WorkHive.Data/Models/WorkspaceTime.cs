﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class WorkspaceTime
{
    public int Id { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; }

    public int WorkspaceId { get; set; }

    public int BookingId { get; set; }

    public string Category { get; set; }

    public virtual Booking Booking { get; set; }

    public virtual Workspace Workspace { get; set; }
}