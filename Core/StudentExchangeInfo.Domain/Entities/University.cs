using Microsoft.AspNetCore.Http;
using StudentExchangeInfo.Domain.Common;
using StudentExchangeInfo.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeInfo.Domain.Entities
{
    public class University : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsRegistred { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }

        public ICollection<AppUser> Users { get; set; }    
    }
}
