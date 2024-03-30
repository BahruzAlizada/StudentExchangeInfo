using StudentExchangeInfo.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Domain.Entities
{
    public class ExchangeProgramCondition : BaseEntity
    {
        public int ExchangeProgramId { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Condition { get; set; }
        public ExchangeProgram ExchangeProgram { get; set; }
    }
}
