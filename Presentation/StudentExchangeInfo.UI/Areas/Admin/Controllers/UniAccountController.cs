using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Application.ViewModels.Student;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UniAccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IUniversityReadRepository universityReadRepository;
        public UniAccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            IUniversityReadRepository universityReadRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.universityReadRepository = universityReadRepository;
        }
        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            double take = 15;
            ViewBag.PageCount = Math.Ceiling(await userManager.Users.Where(x => x.UserRole.Contains("University")).CountAsync() / take);
            ViewBag.CurrentPage = page;
            ViewBag.UniverCount = await userManager.Users.Where(x=>x.UserRole.Contains("University")).CountAsync();

            List<AppUser> users = await userManager.Users.Where(x => x.UserRole.Contains("University")).OrderByDescending(x => x.Created).
                Skip((page - 1) * (int)take).Take((int)take).ToListAsync();
            List<UniversityVM> studentVMs = new List<UniversityVM>();

            foreach (var item in users)
            {
                UniversityVM vm = new UniversityVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    UserName = item.UserName,
                    Email = item.Email,
                    Created = item.Created,
                    Status = item.Status,
                };
                studentVMs.Add(vm);
            }
            return View(studentVMs);
        }
        #endregion

        #region ResetPassword
        public async Task<IActionResult> ResetPassword(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(int? id, ResetPasswordVM resetPassword)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            string token = await userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result = await userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            if (user.Status)
                user.Status = false;
            else
                user.Status = true;

            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
