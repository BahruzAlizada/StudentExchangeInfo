using Microsoft.AspNetCore.Http;
using StudentExchangeInfo.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExchangeInfo.Domain.Entities
{
	public class Slider : BaseEntity
	{
		[Required(ErrorMessage = "Bu xana boş ola bilməz")]
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public string Image { get; set; }
		[NotMapped]
		public IFormFile? Photo { get; set; }
	}
}
