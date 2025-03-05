﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.BuildingBlocks.Exceptions;

namespace WorkHive.Services.Exceptions;

class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string name, object message) : base(name, message)
    {

    }

    public UserNotFoundException(string message) : base(message)
    {
        
    }
}
