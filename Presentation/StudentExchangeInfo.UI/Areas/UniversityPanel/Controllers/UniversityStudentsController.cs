using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels.Student;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;
using StudentExchangeInfo.Persistence.Concrete;

namespace StudentExchangeInfo.UI.Areas.UniversityPanel.Controllers
{
    [Area("UniversityPanel")]
    public class UniversityStudentsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IUniversityReadRepository universityReadRepository;
        public UniversityStudentsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IUniversityReadRepository universityReadRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.universityReadRepository = universityReadRepository;
        }

        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            University university = await universityReadRepository.FindUniversityByNameAsync(user.Name);

            int take = 20;
            ViewBag.PageCount = Math.Ceiling((await userManager.Users.Include(x => x.University).Where(x => x.UniversityId == university.Id && x.Status).CountAsync()) / (double)take);
            ViewBag.CurrentPage = page;

            List<AppUser> users = await userManager.Users.Include(x => x.University).Where(x => x.UniversityId == university.Id && x.Status).
                OrderByDescending(x => x.Id).Skip((page - 1) * take).Take(take).ToListAsync();
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

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.Include(x=>x.University).FirstOrDefaultAsync(x => x.Id==id);
            if(user == null) return BadRequest();

            StudentOtherInformationVM vm = new StudentOtherInformationVM
            {
                FullName = user.Name + " " + user.Surname,
                Email = user.Email,
                UniName = user.University.Name,
                UOMG = user.UOMG,
                IsBacheolor = user.IsBacheolor,
            };

            return View(vm);
        }
        #endregion
    }
}
