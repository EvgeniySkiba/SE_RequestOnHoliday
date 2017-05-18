using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.Repository.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string name { get; set; }
    }
}