using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<University> Universities { get; set;}
        public List<Faq> Faqs { get; set; }
        public List<Blog> Blogs { get; set; }
        public About About { get; set; }
    }
}
