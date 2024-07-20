using Microsoft.AspNetCore.Http;
using StudentExchangeInfo.Domain.Common;
using StudentExchangeInfo.Domain.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeInfo.Domain.Entities
{
    public class Post : BaseEntity
    {
        public int UserId { get; set; }
        public string? Message { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        public string? Image { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime? Updated { get; set;}
        public AppUser User { get; set; }
    }
}
