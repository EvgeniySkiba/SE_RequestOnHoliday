using SE_RequestOnHoliday.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE_RequestOnHoliday.Repository.DTO
{
    /// <summary>
    /// for edit and create 
    /// </summary>
    public class EmployeerDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Имя")]
        public string FirtstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public virtual int RoleId { get; set; }

        [Required]
        [Remote("IsNumberEven", "EmployeerController", ErrorMessage = "В базе данных уже есть запсить с таким логином.")]
        [StringLength(50)]

        [Display(Name = "Логин")]
        public String Login { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Повтор пароля")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}