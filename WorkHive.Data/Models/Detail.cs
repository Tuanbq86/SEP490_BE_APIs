﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class Detail
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Status { get; set; }

    public virtual ICollection<WorkspaceDetail> WorkspaceDetails { get; set; } = new List<WorkspaceDetail>();
}