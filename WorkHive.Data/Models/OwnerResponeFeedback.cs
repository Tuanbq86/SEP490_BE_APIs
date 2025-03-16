﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class OwnerResponeFeedback
{
    public int Id { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public int UserId { get; set; }

    public int OwnerId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ImageResponseFeedback> ImageResponseFeedbacks { get; set; } = new List<ImageResponseFeedback>();

    public virtual WorkspaceOwner Owner { get; set; }

    public virtual User User { get; set; }
}