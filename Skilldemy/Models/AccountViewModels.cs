using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skilldemy.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Zapamiętać tą przeglądarkę?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Pole wymagane - Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole wymagane - Nazwa uzytkownika")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole wymagane - hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pole wymagane - Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Hasło")]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} znaków", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Podane hasła różnią się")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Nazwa użytkownika")]
        [Index(IsUnique = true)]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Numer konta bankowego")]
        [Display(Name = "Numer konta bankowego")]
        public string BankAccountNumber { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Ulica")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Numer domu")]
        [Display(Name = "Numer domu")]
        public string HouseNumber { get; set; }

        [Display(Name = "Numer mieszkania")]
        public string FlatNumber { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Kod pocztowy")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Miejscowość")]
        [Display(Name = "Miejscowość")]
        public string Town { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Pole wymagane - Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} znaków", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Podane hasła różnią się")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Pole wymagane - Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
