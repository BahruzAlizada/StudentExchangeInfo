using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Application.ViewModels.Student;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.UI.Areas.Student.Controllers
{
	[Area("Student")]
	public class ProfileController : Controller
	{
		private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IUniversityReadRepository universityReadRepository;
        public ProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUniversityReadRepository universityReadRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.universityReadRepository = universityReadRepository;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            StudentVM dbVm = new StudentVM
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName,
            };

            University university = await universityReadRepository.GetAsync(x => x.Id == user.UniversityId);
            ViewBag.UniName = university.Name;  

            return View(dbVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Index(string username, StudentVM vm)
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            StudentVM dbVm = new StudentVM
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName,
            };

            University university = await universityReadRepository.GetAsync(x => x.Id == user.UniversityId);
            ViewBag.UniName = university.Name;

            dbVm.Name = vm.Name;
            dbVm.Surname = vm.Surname;
            dbVm.Email = vm.Email;
            dbVm.Username = vm.Username;

            user.Name = vm.Name;
            user.Surname = vm.Surname;
            user.UserName = vm.Username;
            user.Email = vm.Email;

            await userManager.UpdateAsync(user);
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area="default"});
        }
        #endregion

        #region ChangePassword
        public async Task<IActionResult> ChangePassword()
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            ViewBag.username = user.UserName;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePassword)
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            ViewBag.username = user.UserName;

            bool passwordIsValid = await userManager.CheckPasswordAsync(user, changePassword.OldPassword);
            if (passwordIsValid)
            {
                string newPasswordHash = userManager.PasswordHasher.HashPassword(user, changePassword.NewPassword);
                user.PasswordHash = newPasswordHash;
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
                await signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account", new { area = "default" });
            }
            else
            {
                ModelState.AddModelError("OldPassword", "Köhnə şifrə yanlışdır !");
                return View();
            }
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "default" });
        }
        #endregion
    }
}
