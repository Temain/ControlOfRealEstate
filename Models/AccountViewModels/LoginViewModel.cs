using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "¬ведите адрес электронной почты")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "¬ведите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
