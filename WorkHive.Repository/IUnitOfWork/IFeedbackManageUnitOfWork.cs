﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Repositories.IRepositories;

namespace WorkHive.Repositories.IUnitOfWork
{
    public interface IFeedbackManageUnitOfWork
    {
        IFeedbackManageUnitOfWork Feedback { get; }

        IOwnerResponseFeedbackRepository OwnerResponseFeedback { get; }

        IImageFeedbackRepository ImageFeedback { get; }

        IImageRepository Image { get; }

        IImageResponseFeedbackRepository ImageResponseFeedback { get; }

        IUserRepository User { get; }

        IWorkspaceOwnerRepository WorkspaceOwner { get; }

        public int Save();
        public Task<int> SaveAsync();

    }
}
