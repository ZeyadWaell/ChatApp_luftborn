﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string userName);

    }
}
