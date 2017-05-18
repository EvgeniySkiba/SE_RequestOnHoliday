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
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Окончание отпуска")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}