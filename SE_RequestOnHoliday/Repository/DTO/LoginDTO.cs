using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.ViewModel
{
    public class LoginDTO
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName{ get; set; }

        [Required]
        [MinLength(4)]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}