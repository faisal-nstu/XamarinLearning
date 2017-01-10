﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityMimicXMApp.Services
{
    public interface IDataService
    {
        Task<UserAccountModel> Login(UserLoginModel loginData);
    }
}