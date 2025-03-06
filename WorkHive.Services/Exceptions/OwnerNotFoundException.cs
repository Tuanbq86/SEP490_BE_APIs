﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.Exceptions;

namespace WorkHive.Services.Exceptions
{
    class OwnerNotFoundException : NotFoundException
    {
        public OwnerNotFoundException(string name, string message) : base(name, message)
        {

        }

    }

}
