﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Data.Base;
using WorkHive.Data.Models;

namespace WorkHive.Repositories.IRepositories;

public interface IWorkspaceFacilityRepository : IGenericRepository<WorkspaceFacility>
{
    public Task CreateWorkspaceFacilitiesAsync(List<WorkspaceFacility> workspaceFacilities);

    public Task<List<WorkspaceFacility>> GetWorkspaceFacilitiesByWorkspaceIdAsync(int workspaceId);

    Task<List<WorkspaceFacility>> GetByWorkspaceIdAsync(int workspaceId);

    Task DeleteWorkspaceFacilitiesAsync(List<WorkspaceFacility> workspaceFacilities);
}

