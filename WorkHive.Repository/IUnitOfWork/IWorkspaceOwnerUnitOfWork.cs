﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Repositories.IRepositories;

namespace WorkHive.Repositories.IUnitOfWork;

public interface IWorkspaceOwnerUnitOfWork
{

    IWorkspaceOwnerRepository WorkspaceOwner { get; }
    public int Save();
    public Task<int> SaveAsync();
}
