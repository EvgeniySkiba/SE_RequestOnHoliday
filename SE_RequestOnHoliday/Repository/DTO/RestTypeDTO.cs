using System.ComponentModel.DataAnnotations;

namespace SE_RequestOnHoliday.Repository.DTO
{
    public class RestTypeDTO
	{
        public int Id { get; set; }

        [Display(Name = "Тип отпуска")]
        public string Name { get; set; }
	}
}