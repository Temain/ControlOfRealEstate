using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "������� ��� ������������")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "������� ����� ����������� �����")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required(ErrorMessage = "������� ������")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
