using StudentExchangeInfo.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Domain.Entities
{
	public class Slider : BaseEntity
	{
		[Required(ErrorMessage = "Bu xana boş ola bilməz")]
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public string Image { get; set; }
	}
}
