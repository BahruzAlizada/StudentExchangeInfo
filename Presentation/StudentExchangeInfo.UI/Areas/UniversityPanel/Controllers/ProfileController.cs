using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Application.ViewModels.Student;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;
using StudentExchangeInfo.Persistence.Concrete;

namespace StudentExchangeInfo.UI.Areas.UniversityPanel.Controllers
{
	[Area("UniversityPanel")]
	public class ProfileController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
        private readonly IUniversityReadRepository universityReadRepository;
		public ProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IUniversityReadRepository universityReadRepository)
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

            University university = await universityReadRepository.FindUniversityByNameAsync(user.Name);

			if (user == null) return BadRequest();

			UniversityVM dbUni = new UniversityVM
			{
				Name = user.Name,
				Email = user.Email,
                Image = university.Image,
                StudentCount = await universityReadRepository.FindUniversityStudentCountAsync(user.Name)
			};

			return View(dbUni);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Index(UniversityVM uni)
		{
			AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
			if (user == null) return BadRequest();

            University university = await universityReadRepository.FindUniversityByNameAsync(user.Name);

            UniversityVM dbUni = new UniversityVM
			{
				Name = user.Name,
				Email = user.Email,
                Image = university.Image,
                StudentCount = await universityReadRepository.FindUniversityStudentCountAsync(user.Name)
            };

			dbUni.Email = uni.Email;
			user.Email = uni.Email;

			await userManager.UpdateAsync(user);
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "default" });
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

        #region UniversityStudents
        public async Task<IActionResult> UniversityStudents(int page = 1)
        {
            using var context = new Context();

            double take = 20;


            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            University university = await universityReadRepository.FindUniversityByNameAsync(user.Name);
            List<AppUser> users = await userManager.Users.Include(x => x.University).Where(x => x.UniversityId == university.Id && x.Status).
                OrderByDescending(x=>x.Id).ToListAsync();
            List<StudentVM> studentVMs = new List<StudentVM>();

            foreach (var item in users)
            {
                StudentVM vm = new StudentVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Email = item.Email,
                    Created = item.Created,
                };
                studentVMs.Add(vm);
            }
            

            return View(studentVMs);
        }
        #endregion

    }
}
