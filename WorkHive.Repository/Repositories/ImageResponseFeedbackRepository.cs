﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Data.Base;
using WorkHive.Data.Models;
using WorkHive.Repositories.IRepositories;

namespace WorkHive.Repositories.Repositories
{
    public class ImageResponseFeedbackRepository : GenericRepository<ImageResponseFeedback>, IImageResponseFeedbackRepository
    {
        public ImageResponseFeedbackRepository(){ }
        public ImageResponseFeedbackRepository(WorkHiveContext context) => _context = context;
    }
}
