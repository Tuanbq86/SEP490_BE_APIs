﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Data.Base;
using WorkHive.Data.Models;

namespace WorkHive.Repositories.IRepositories
{
    public interface IOwnerResponseFeedbackRepository : IGenericRepository<OwnerResponseFeedback>
    {
        public Task<OwnerResponseFeedback?> GetResponseFeedbackById(int Id);
        public Task<List<OwnerResponseFeedback>> GetAllResponseFeedbacks();
        public Task<List<OwnerResponseFeedback>> GetResponseFeedbacksByOwnerId(int ownerId);
        Task<List<OwnerResponseFeedback>> GetResponseFeedbacksByUserId(int userId);
        Task<OwnerResponseFeedback?> GetFirstResponseFeedbackByFeedbackId(int feedbackId);

    }
}
