using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.ViewModel
{
    public class RestVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Status { get; set; }



    }
}