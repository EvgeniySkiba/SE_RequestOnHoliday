using SE_RequestOnHoliday.Models;
using SE_RequestOnHoliday.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.Repository.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private EmployersContext _db = new EmployersContext();
    
        public bool ValidateUser(string userName, string pass)
        {           
            try
            {
                Employer user = (from u in _db.Employers
                                 where u.Login == userName && u.Password == pass
                                 select u).FirstOrDefault();
                if (user != null)
                    return true;

                return false;                   
            }
            catch
            {
                return  false;
            }
        }
    }
}