using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Controllers
{
    public class NewsController : Controller
    {
        private readonly IBlogReadRepository blogReadRepository;
        public NewsController(IBlogReadRepository blogReadRepository)
        {
            this.blogReadRepository = blogReadRepository;
        }

        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.PageCount = await blogReadRepository.PageCountAsync(10);
            ViewBag.CurrentPage = page;

            List<Blog> blogs = await blogReadRepository.GetBlogsWithPageAsync(10, page);
            return View(blogs);
        }
        #endregion

        #region Detail
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = blogReadRepository.Get(x => x.Id == id);
            if (blog == null) return BadRequest();

            return View(blog);
        }
        #endregion
    }
}
