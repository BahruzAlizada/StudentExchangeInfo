using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Application.ViewModels.Student;
using StudentExchangeInfo.Application.ViewModels.University;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IUniversityReadRepository universityReadRepository;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IUniversityReadRepository universityReadRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.universityReadRepository = universityReadRepository;
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser? user = await userManager.FindByEmailAsync(login.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "Email və yaxud Şifrə yanlışdır");
                return View();
            }
            if (!user.Status)
            {
                ModelState.AddModelError("", "Sizin hesabınız bloklanıb");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.
                PasswordSignInAsync(user, login.Password,
                login.IsRemember, true);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email və yaxud Şifrə yanlışdır");
                return View();
            }
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Sizin hesabınız 1 dəqiqəlik blokandı.");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region StudentRegister
        public async Task<IActionResult> StudentRegister()
        {
            ViewBag.Universities = await universityReadRepository.GetActiveUniversitiesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> StudentRegister(StudentRegisterVM studentRegister,int uniId)
        {
            ViewBag.Universities = await universityReadRepository.GetActiveUniversitiesAsync();

            AppUser user = new AppUser
            {
                Name = studentRegister.Name,
                Surname = studentRegister.Surname,
                UserName = studentRegister.UserName,
                Email = studentRegister.Email,
                UserRole = "Student",
                UniversityId = uniId
            };

            IdentityResult result = await userManager.CreateAsync(user,studentRegister.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            var RoleName = await roleManager.FindByNameAsync("Student");
            await userManager.AddToRoleAsync(user, RoleName.Name);
            await signInManager.SignInAsync(user, studentRegister.IsRemember);

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region UniversityRegister
        public async Task<IActionResult> UniversityRegister()
        {
            ViewBag.Universities = await universityReadRepository.GetActiveUniversitiesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> UniversityRegister(int uniId, UniversityRegisterVM register)
        {
            ViewBag.Universities = await universityReadRepository.GetActiveUniversitiesAsync();

            University university = await universityReadRepository.GetAsync(x => x.Id == uniId);

            AppUser uni = new AppUser
            {
                Name = university.Name,
                Surname = "XXX",
                UserName = Guid.NewGuid().ToString("N").Substring(0, 8),
                Email = register.Email,
                IsUser = false,
                UserRole = "University"
            };

            IdentityResult result = await userManager.CreateAsync(uni, register.Password);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return View();
			}

			var RoleName = await roleManager.FindByNameAsync("University");
			await userManager.AddToRoleAsync(uni, RoleName.Name);
			await signInManager.SignInAsync(uni, register.IsRemember);

			return RedirectToAction("Index","Home");
        }
        #endregion

        #region SignOut
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
