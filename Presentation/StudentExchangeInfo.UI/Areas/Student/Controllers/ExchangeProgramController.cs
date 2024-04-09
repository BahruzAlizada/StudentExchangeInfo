using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;
using StudentExchangeInfo.Persistence.Concrete;

namespace StudentExchangeInfo.UI.Areas.Student.Controllers
{
    [Area("Student")]
    public class ExchangeProgramController : Controller
    {
        private readonly IExchangeProgramReadRepository exchangeProgramReadRepository;
        private readonly IUniversityReadRepository universityReadRepository;
        private readonly UserManager<AppUser> userManager;
        public ExchangeProgramController(IExchangeProgramReadRepository exchangeProgramReadRepository, IUniversityReadRepository universityReadRepository,
            UserManager<AppUser> userManager)
        {
            this.exchangeProgramReadRepository = exchangeProgramReadRepository;
            this.universityReadRepository = universityReadRepository;
            this.userManager = userManager;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();
          
            University university = await universityReadRepository.GetAsync(x => x.Id == user.UniversityId);
            AppUser? uniUser = await userManager.Users.FirstOrDefaultAsync(x => x.Name == university.Name);
            if(uniUser == null) return BadRequest();

            ViewBag.Image = university.Image;
            ViewBag.Name = university.Name;

            List<ExchangeProgram> exchangePrograms = await exchangeProgramReadRepository.GetActiveExchangeProgramsByUniversityAsync(uniUser.Id);
            return View(exchangePrograms);
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            University university = await universityReadRepository.GetAsync(x => x.Id == user.UniversityId);
            ViewBag.Image = university.Image;

            if (id == null) return NotFound();
            ExchangeProgram? exchangeProgram = await exchangeProgramReadRepository.GetExchangeProgramWithUserAsync(id);
            if (exchangeProgram == null) return BadRequest();

            return View(exchangeProgram);
        }
        #endregion
    }
}
