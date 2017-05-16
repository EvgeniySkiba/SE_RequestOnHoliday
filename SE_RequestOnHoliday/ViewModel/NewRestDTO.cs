using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.ViewModel
{
    public class NewRestDTO
    {
        public string login { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}