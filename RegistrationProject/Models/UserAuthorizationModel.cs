using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationProject.Models
{
    public class UserAuthorizationModel
    {
        [Display(Name = "Iм'я користувача")]
        [Required(ErrorMessage = "Введіть ім'я користувача")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; }
    }
}