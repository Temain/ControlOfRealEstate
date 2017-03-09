using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlOfRealEstate.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "���������� ������� ����� ����������� �����")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "���������� ������� ��� ������������")]
        public string UserName { get; set; }

        public string DisplayName { get; set; }
    }
}
