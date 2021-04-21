using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WaveUP.Domain.Entities
{
    public class Instrument
    {
        [HiddenInput(DisplayValue = false)]
        public int InstrumentId { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Введіть назву товару")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Опис")]
        [Required(ErrorMessage = "Введіть опис товару")]
        public string Description { get; set; }

        [Display(Name = "Категорія")]
        [Required(ErrorMessage = "Введіть категорію товару")]
        public string Category { get; set; }

        [Display(Name = "Ціна")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Введіть коректну ціну")]
        public decimal Price { get; set; }
    }
}
