using Microsoft.AspNetCore.Mvc;

namespace StudentExchangeInfo.UI.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
