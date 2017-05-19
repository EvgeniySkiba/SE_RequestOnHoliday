using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.Enums
{
    public enum Status:int
    {
        [Display(Name = "Утверждено")]
        Applied = 2,

        [Display(Name = "Не утверждено")]
        NotApplied =1 
    }
}