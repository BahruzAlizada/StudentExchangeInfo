using StudentExchangeInfo.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Domain.Entities
{
	public class Faq : BaseEntity
	{
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Quetsion { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Answer { get; set; }

        public int? CategoryId { get; set; }
        public FaqCategory? Category { get; set; }
    }
}
