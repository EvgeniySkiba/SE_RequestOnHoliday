using SE_RequestOnHoliday.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.Repository.DTO
{
    public class UsersRequestsDTO
    {
        public int EmployerID;

        [DataType(DataType.Date)]
        [Display(Name = "Начало отпуска")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Окончание отпуска")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Тип отпуска")]
        public int RestTypeId { get; set; }
    }
}