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

}
