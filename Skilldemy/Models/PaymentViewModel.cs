using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Skilldemy.Models
{
    public class PaymentViewModel
    {
        // Merchant Id
        public int Id = 1010;

        // Security code
        public string Code = "demo";

        // Course price
        public decimal Amount { get; set; }

        // Course title
        public string Description { get; set; }

        // Checksum, in our case its course id
        public int Crc { get; set; }

        public string Return_url { get; set; }

        public string Return_error_url { get; set; }

        public string Language { get; set; }

        // Customer concatenated name
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Imię")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Nazwisko")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Adres email")]
        [Display(Name = "Adres email")]
        public string EmailAddress { get; set; }
    }
}