﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PP.BL.Interfaces
{
    public interface IAuthBl
    {
        Task<IdentityResult> RegisterUser(IdentityUser userModel);


    }
}