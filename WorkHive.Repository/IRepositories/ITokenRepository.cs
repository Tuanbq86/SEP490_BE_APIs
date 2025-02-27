﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Data.Models;

namespace WorkHive.Repositories.IRepositories;

public interface ITokenRepository
{
    public string GenerateJwtToken(User user);
    public string GenerateJwtToken(WorkspaceOwner Owner);
}
