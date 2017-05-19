using SE_RequestOnHoliday.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SE_RequestOnHoliday.Repository.DTO;
using SE_RequestOnHoliday.Models;

namespace SE_RequestOnHoliday.Repository.Concrete
{
    public class RestTypeRepository : IRestTypeRepository
    {
        private EmployersContext _db = new EmployersContext();

        public IEnumerable<RestTypeDTO> list()
        {
            IEnumerable<RestTypeDTO> entries = _db.RestTypes.Select(p => new RestTypeDTO
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
            return entries;
        }
    }
}