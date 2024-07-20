using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;
using StudentExchangeInfo.Infrastructure.Helpers;

namespace StudentExchangeInfo.UI.Areas.UniversityPanel.Controllers
{
    [Area("UniversityPanel")]
    [Authorize(Roles = "University")]
    public class PostController : Controller
    {
        private readonly IPostReadRepository postReadRepository;
        private readonly IPostWriteRepository postWriteRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IWebHostEnvironment env;
        public PostController(IPostReadRepository postReadRepository, IPostWriteRepository postWriteRepository,
            UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            this.postReadRepository = postReadRepository;
            this.postWriteRepository = postWriteRepository;
            this.userManager = userManager;
            this.env = env;
        }

        #region Index
        public async Task<IActionResult> Index(int page=1)
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            ViewBag.PageCount = await postReadRepository.PostPageCountAsync(20, user.Id);
            ViewBag.CurrentPage = page;

            List<Post> posts = await postReadRepository.GetPostsWithPageAsync(user.Id,20,page);
            return View(posts);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Post post)
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            if(post.Image is null && post.Message is null)
            {
                ModelState.AddModelError("", "Şəkil və şərh eyni anda hər ikisi boş ola bilməz");
                return View();
            }

            #region Image
            if(post.Image != null)
            {
                if (post.Photo is null)
                {
                    ModelState.AddModelError("Photo", "Bu xana boş ola bilməz");
                    return View();
                }
                if (!post.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yalnız şəkil tipli fayl");
                    return View();
                }
                if (post.Photo.IsOlder256Kb())
                {
                    ModelState.AddModelError("Photo", "Maksimum 256Kb");
                    return View();
                }
                string folder = Path.Combine(env.WebRootPath, "assets", "img", "post");
                post.Image = await post.Photo.SaveFileAsync(folder);
            }
            #endregion

            post.UserId = user.Id;

            await postWriteRepository.AddAsync(post);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Post post = postReadRepository.Get(x => x.Id == id);
            if (post == null) return BadRequest();

            postWriteRepository.Delete(post);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int? id)
        {
            if(id== null) return NotFound();
            Post post = postReadRepository.Get(x => x.Id == id);
            if(post == null) return BadRequest();

            postWriteRepository.Activity(post);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
