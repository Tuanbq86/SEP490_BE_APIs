﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class WorkspaceDetail
{
    public int Id { get; set; }

    public string Status { get; set; }

    public int WorkspaceId { get; set; }

    public int DetailId { get; set; }

    public virtual Detail Detail { get; set; }

    public virtual Workspace Workspace { get; set; }
}