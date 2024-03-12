

using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Application.ViewModels.University
{
    public class UniversityRegisterVM
    {
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email ünvanınızı düzgün qeyd edin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifrə düzgün deyil ")]
        public string CheckPassword { get; set; }
        public int UniId { get; set; }
        public bool IsRemember { get; set; }
    }
}
