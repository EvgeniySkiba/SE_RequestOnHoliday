using SE_RequestOnHoliday.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.ViewModel
{
    public class RestVM
    {
        public int Id { get; set; }

        [Display(Name ="Сотрудник")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата окончания")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Статус")]
        public Status Status { get; set; }

    }
}