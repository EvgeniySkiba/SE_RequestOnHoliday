using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.Repository.Abstract
{
    public interface IAuthRepository
    {
        bool ValidateUser(string userName, string pass);
    }
}