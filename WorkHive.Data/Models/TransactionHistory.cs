﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class TransactionHistory
{
    public int Id { get; set; }

    public decimal? Amount { get; set; }

    public string Status { get; set; }

    public string Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string Title { get; set; }

    public string BankName { get; set; }

    public string BankNumber { get; set; }

    public string BankAccountName { get; set; }

    public decimal? BeforeTransactionAmount { get; set; }

    public decimal? AfterTransactionAmount { get; set; }

    public virtual ICollection<OwnerTransactionHistory> OwnerTransactionHistories { get; set; } = new List<OwnerTransactionHistory>();

    public virtual ICollection<UserTransactionHistory> UserTransactionHistories { get; set; } = new List<UserTransactionHistory>();
}