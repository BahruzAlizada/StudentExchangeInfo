using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Application.ViewModels.Student;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IUniversityReadRepository universityReadRepository;
        public StudentController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
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
            ViewBag.PageCount = Math.Ceiling(await userManager.Users.Where(x=>x.IsUser).CountAsync() / take);
            ViewBag.CurrentPage = page;
            ViewBag.StudentsCount = await userManager.Users.Where(x => x.IsUser).CountAsync();

            List<AppUser> users = await userManager.Users.Include(x=>x.University).Where(x=>x.IsUser).OrderByDescending(x=>x.Created).
                Skip((page-1)*(int)take).Take((int)take).ToListAsync();
            List<StudentVM> studentVMs = new List<StudentVM>();

            foreach (var item in users)
            {
                StudentVM vm = new StudentVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Username = item.UserName,
                    Email = item.Email,
                    Created = item.Created,
                    Status = item.Status,
                    UniverName = item.University.Name
                };
                studentVMs.Add(vm);
            }
            return View(studentVMs);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Universities = await universityReadRepository.GetActiveUniversitiesAsync();

            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            StudentVM dbStu = new StudentVM
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName,
                UniId = user.UniversityId
            };

            return View(dbStu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, StudentVM stu, int uniId)
        {
            ViewBag.Universities = await universityReadRepository.GetActiveUniversitiesAsync();

            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            StudentVM dbStu = new StudentVM
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName,
                UniId = user.UniversityId
            };

            dbStu.Id = stu.Id;
            dbStu.Name = stu.Name;
            dbStu.Surname = stu.Surname;
            dbStu.Username = stu.Username;
            dbStu.Email = stu.Email;

            user.Id = stu.Id;
            user.Name = stu.Name;
            user.Surname = stu.Surname;
            user.UserName = stu.Username;
            user.Email = stu.Email;
            user.UniversityId = uniId;

            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
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

        public async Task<IActionResult> ResetPassword(int? id,ResetPasswordVM resetPassword)
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
            if(user == null) return BadRequest();

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
