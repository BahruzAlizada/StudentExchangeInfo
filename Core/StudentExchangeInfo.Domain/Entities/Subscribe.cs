using StudentExchangeInfo.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Domain.Entities
{
	public class Subscribe : BaseEntity
	{
		[Required(ErrorMessage = "Bu xana boş ola bilməz")]
		[DataType(DataType.EmailAddress,ErrorMessage = "Email ünvanınızı düzgün daxil edin")]
		public string Email { get; set; }
	}
}
