﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class OwnerWithdrawalRequest
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int WorkspaceOwnerId { get; set; }

    public int? UserId { get; set; }

    public string ManagerResponse { get; set; }

    public string BankName { get; set; }

    public string BankNumber { get; set; }

    public string BankAccountName { get; set; }

    public decimal? Balance { get; set; }

    public virtual User User { get; set; }

    public virtual WorkspaceOwner WorkspaceOwner { get; set; }
}