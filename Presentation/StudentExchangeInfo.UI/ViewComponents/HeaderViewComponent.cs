using Microsoft.AspNetCore.Mvc;

namespace StudentExchangeInfo.UI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
