﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Data.Base;
using WorkHive.Data.Models;
using WorkHive.Repositories.IRepositories;

namespace WorkHive.Repositories.Repositories;

public class BeverageRepository : GenericRepository<Beverage>, IBeverageRepository
{
    public BeverageRepository() { }

    public BeverageRepository(WorkHiveContext context) => _context = context;



    public async Task<List<Beverage>> GetBeveragesByOwnerIdAsync(int OwnerId)
    {
        return await _context.Beverages
            .Where(ws => ws.OwnerId == OwnerId)
            .ToListAsync();
    }
}
