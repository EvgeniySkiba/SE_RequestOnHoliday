using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.ViewModel
{
    public class LoginVM
    {
        [Required]
        public string UserName{ get; set; }

        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}