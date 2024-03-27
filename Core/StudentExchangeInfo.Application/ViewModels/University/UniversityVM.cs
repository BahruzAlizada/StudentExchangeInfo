
using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Application.ViewModels
{
    public class UniversityVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Elektron-poçt adresini düzgün qeyd edin")]
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }

        public string Image { get; set; }
        public int StudentCount { get; set; }
    }
}
