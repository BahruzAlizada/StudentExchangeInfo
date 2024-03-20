﻿
using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Application.ViewModels
{
    public class ChangePasswordVM
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [Compare("NewPassword", ErrorMessage = "Şifrəni düzgün daxil edin")]
        public string ConfirmPassword { get; set; }
    }
}
