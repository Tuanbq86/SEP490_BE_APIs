﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkHive.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Status { get; set; }

    public string Avatar { get; set; }

    public string Location { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int RoleId { get; set; }

    public string Sex { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<CustomerWallet> CustomerWallets { get; set; } = new List<CustomerWallet>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OwnerWithdrawalRequest> OwnerWithdrawalRequests { get; set; } = new List<OwnerWithdrawalRequest>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Role Role { get; set; }

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}