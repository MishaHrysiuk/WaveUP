using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WaveUP.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Вкажіть ваше імя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вкажіть перший адрес доставки")]
        [Display(Name = "Перший адрес")]
        public string Line1 { get; set; }
        [Display(Name = "Другий адрес")]
        public string Line2 { get; set; }
        [Display(Name = "Третій адрес")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Вкажіть місто")]
        [Display(Name = "Місто")]
        public string City { get; set; }

        [Required(ErrorMessage = "Вкажіть країну")]
        [Display(Name = "Країна")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
