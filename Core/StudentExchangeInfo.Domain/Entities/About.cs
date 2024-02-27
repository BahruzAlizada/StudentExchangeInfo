using StudentExchangeInfo.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Domain.Entities
{
	public class About : BaseEntity
	{
		[Required(ErrorMessage = "Bu xana boş ola bilməz")]
		public string Description { get; set; }
	}
}
