using Microsoft.AspNetCore.Http;
using StudentExchangeInfo.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeInfo.Domain.Entities
{
    public class Blog : BaseEntity
    {
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
