﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Data.Base;
using WorkHive.Data.Models;

namespace WorkHive.Repositories.IRepositories;

public interface IPolicyRepository : IGenericRepository<Policy>
{
    public Task CreatePoliciesAsync(List<Policy> policies);

    public Task<List<Policy>> GetPoliciesByWorkspaceIdAsync(int workspaceId);

    public Task DeletePoliciesByIdsAsync(List<int> policyIds);
}
