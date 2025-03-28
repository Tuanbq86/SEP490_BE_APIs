﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Data.Base;
using WorkHive.Data.Models;

namespace WorkHive.Repositories.IRepositories;

public interface IAmenityRepository : IGenericRepository<Amenity>
{
    public Task<List<Amenity>> GetAmenitiesByOwnerIdAsync(int ownerId);
    public Task<List<NumberOfBookingAmenitiesDTO>> GetNumberOfBookingAmenity(int OwnerId);

}

public class NumberOfBookingAmenitiesDTO
{
    public int AmenityId { get; set; }
    public string? AmenityName { get; set; }
    public decimal? UnitPrice { get; set; }
    public string? Img_Url { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public int NumberOfBooking { get; set; }
}