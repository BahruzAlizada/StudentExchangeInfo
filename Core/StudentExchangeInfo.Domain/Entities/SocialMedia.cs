using StudentExchangeInfo.Domain.Common;

namespace StudentExchangeInfo.Domain.Entities
{
    public class SocialMedia : BaseEntity
    {
        public string Icon { get; set; }
        public string Link { get; set; }
    }
}
