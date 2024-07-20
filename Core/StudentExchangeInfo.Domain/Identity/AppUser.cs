using Microsoft.AspNetCore.Identity;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Domain.Identity
{
	public class AppUser : IdentityUser<int>
	{
		public int? UniversityId { get; set; }

		public string Name { get; set; }
		public string Surname { get; set; }
		public string UserRole { get; set; }

		public bool Status { get; set; } = true;
		public bool IsUser { get; set; } = true;
		public bool? IsBacheolor { get; set; } = true;
		public double? UOMG { get; set; }
		public DateTime Created { get; set; } = DateTime.UtcNow.AddHours(4);

		public University? University { get; set; }
		public List<ExchangeProgram>? ExchangePrograms { get; set; }
		public List<Post>? Posts { get; set; }
	}
}
