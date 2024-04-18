using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin,ContactManager")]
	public class SubscribeController : Controller
    {
        private readonly ISubscribeReadRepository subscribeReadRepository;
        private readonly ISubscribeWriteRepository subscribeWriteRepository;
        public SubscribeController(ISubscribeReadRepository subscribeReadRepository, ISubscribeWriteRepository subscribeWriteRepository)
        {
            this.subscribeReadRepository = subscribeReadRepository;
            this.subscribeWriteRepository = subscribeWriteRepository;
        }

        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            double take = 20;
            ViewBag.PageCount = await subscribeReadRepository.PageCountSubscribeAsync(take);
            ViewBag.CurrentPage = page;

            List<Subscribe> subscribes = await subscribeReadRepository.GetSubscribesWithPagingAsync((int)take, page);
            return View(subscribes);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if(id==null) return NotFound();
            Subscribe? subscribe = subscribeReadRepository.Get(x=>x.Id == id);
            if(subscribe == null) return BadRequest();

            subscribeWriteRepository.Delete(subscribe);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
