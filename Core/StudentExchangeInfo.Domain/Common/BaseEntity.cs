namespace StudentExchangeInfo.Domain.Common
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public bool Status { get; set; } = true;
		public DateTime Created { get; set; } = DateTime.UtcNow.AddHours(4);
	}
}
