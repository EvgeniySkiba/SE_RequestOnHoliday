using SE_RequestOnHoliday.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.Repository.Abstract
{
    public interface IRoleRepository
    {
        IEnumerable<RoleDTO> getAll();
        RoleDTO get(int id);
    }
}