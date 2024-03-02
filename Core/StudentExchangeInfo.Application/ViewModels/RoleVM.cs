using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Application.ViewModels
{
    public class RoleVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string RoleName { get; set; }
    }
}
