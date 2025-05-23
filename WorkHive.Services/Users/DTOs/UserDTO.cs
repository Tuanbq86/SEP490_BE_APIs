﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHive.Services.Users.DTOs;

public class UserDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Status { get; set; }

    public string Avatar { get; set; }

    public string Location { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string RoleName { get; set; }

    public string Sex { get; set; }
    public int? IsBan { get; set; }
}
