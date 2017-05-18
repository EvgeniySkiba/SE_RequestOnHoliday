using SE_RequestOnHoliday.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SE_RequestOnHoliday.Repository.DTO;
using SE_RequestOnHoliday.Models;

namespace SE_RequestOnHoliday.Repository.Concrete
{

    public class RoleRepository : IRoleRepository
    {
        private EmployersContext db = new EmployersContext();

        public RoleDTO get(int id)
        {

            var role = db.Roles
                  .Where(u => u.Id == id)
                  .Select(u => new RoleDTO { Id = u.Id, name = u.Name })
                  .FirstOrDefault(); // query is executed here

            return role;
        }

        public IEnumerable<RoleDTO> getAll()
        {
            IEnumerable<RoleDTO> entries = db.Roles.Select(p => new RoleDTO
            {
                Id = p.Id,
                name = p.Name
            }).ToList();

            return entries;
        }
    }
}