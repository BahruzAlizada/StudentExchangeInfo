using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Infrastructure.Abstract;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogReadRepository blogReadRepository;
        private readonly IBlogWriteRepository blogWriteRepository;
        private readonly IWebHostEnvironment env;
        private readonly IPhotoService photoService;
        public BlogController(IBlogReadRepository blogReadRepository, IBlogWriteRepository blogWriteRepository,
            IWebHostEnvironment env, IPhotoService photoService)
        {
            this.blogReadRepository = blogReadRepository;
            this.blogWriteRepository = blogWriteRepository;
            this.env = env;
            this.photoService = photoService;
        }

        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.PageCount = await blogReadRepository.PageCountAsync(18);
            ViewBag.CurrentPage = page;

            List<Blog> blogs = await blogReadRepository.GetBlogsWithPageAsync(18, page);
            return View(blogs);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Blog blog)
        {
            #region Image
            (bool isValid, string errorMessage) = await photoService.PhotoChechkValidatorAsync(blog.Photo, false, true);
            if (!isValid)
            {
                ModelState.AddModelError("Photo", errorMessage);
                return View();
            }
            string folder = Path.Combine(env.WebRootPath,"assets", "img", "blogs");
            blog.Image = await photoService.SavePhotoAsync(blog.Photo, folder);
            #endregion

            await blogWriteRepository.AddAsync(blog);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = blogReadRepository.Get(x => x.Id == id);
            if(blog == null) return BadRequest();

            blogWriteRepository.Activity(blog);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = blogReadRepository.Get(x => x.Id == id);
            if (blog == null) return BadRequest();

            blogWriteRepository.Delete(blog);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
