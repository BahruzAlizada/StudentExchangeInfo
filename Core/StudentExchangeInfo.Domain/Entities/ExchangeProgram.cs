using StudentExchangeInfo.Domain.Common;
using StudentExchangeInfo.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Domain.Entities
{
    public class ExchangeProgram : BaseEntity
    {
        public int AppUserId { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string ExchangeName { get; set; }
        public string Description { get; set; }

        public AppUser AppUser { get; set; }
        public List<ExchangeProgramCondition> Conditions { get; set; }
        public List<ExchangeProgramDocument> Documents { get; set; }
    }
}
