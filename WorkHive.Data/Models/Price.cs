﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class Price
{
    public int Id { get; set; }

    public decimal? Price1 { get; set; }

    public string Category { get; set; }

    public virtual ICollection<WorkspacePrice> WorkspacePrices { get; set; } = new List<WorkspacePrice>();
}