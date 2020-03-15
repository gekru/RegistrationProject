using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationProject.Models
{
    public class UserRegistrationModel
    {
        
        [Display(Name = "Iм'я користувача")]
        [Required(ErrorMessage = "Введіть ім'я користувача")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Iм'я повинно бути щонайменше 2 лiтери")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; }

        [Display(Name = "Підтвердження пароля")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введіть пароль")]
        [Compare("Password", ErrorMessage = "Не співпадає з паролем")]
        public string ConfirmPassword { get; set; }

        
        [Display(Name = "Електронна адреса")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Введіть електронну адресу")]
        public string Name { get; set; }

    }
}