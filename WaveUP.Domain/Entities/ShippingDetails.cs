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
        //First Name
        [Required(ErrorMessage = "Вкажіть ім'я")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        //Last Name
        [Required(ErrorMessage = "Вкажіть прізвище")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        
        // Region
        [Required(ErrorMessage = "Вкажіть область")]
        [Display(Name = "Область")]
        public string Region { get; set; }

        //Town
        [Required(ErrorMessage = "Вкажіть населенний пункт")]
        [Display(Name = "Населенний пункт")]
        public string Town { get; set; }

        //Address
        [Required(ErrorMessage = "Вкажіть адресу")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }

        //House number
        [Required(ErrorMessage = "Вкажіть номер будинку")]
        [Display(Name = "Номер будинку")]
        public string HouseNumber { get; set; }

        //Apartment number 
        [Display(Name = "Номер квартири")]
        public string ApartmentNumber { get; set; }

        //Phone тumber 
        [Required(ErrorMessage = "Вкажіть номер телефону")]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; }
    }
}
