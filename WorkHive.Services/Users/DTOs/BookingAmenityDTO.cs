﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHive.Services.Users.DTOs;

public class BookingAmenityDTO
{
    public int Id { get; set; }
    public int? Quantity { get; set; }

    public BookingAmenityDTO()
    {
        
    }

    public BookingAmenityDTO(int Id, int? Quantity)
    {
        this.Id = Id;
        this.Quantity = Quantity;
    }
}
