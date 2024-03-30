using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.UI.Areas.UniversityPanel.Controllers
{
    [Area("UniversityPanel")]
    public class ExchangeProgramController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IExchangeProgramReadRepository exchangeProgramReadRepository;
        private readonly IExchangeProgramWriteRepository exchangeProgramWriteRepository;
        private readonly IUniversityReadRepository universityReadRepository;
        public ExchangeProgramController(UserManager<AppUser> userManager, IExchangeProgramReadRepository exchangeProgramReadRepository,
            IExchangeProgramWriteRepository exchangeProgramWriteRepository, IUniversityReadRepository universityReadRepository)
        {
            this.userManager = userManager;
            this.exchangeProgramReadRepository = exchangeProgramReadRepository;
            this.exchangeProgramWriteRepository = exchangeProgramWriteRepository;
            this.universityReadRepository = universityReadRepository;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            University? university = await universityReadRepository.FindUniversityByNameAsync(user.Name);
            if (university == null) return BadRequest();

            ViewBag.Image = university.Image;
            ViewBag.Name = user.Name;

            List<ExchangeProgram> exchangePrograms = await exchangeProgramReadRepository.GetActiveExchangeProgramsByUniversityAsync(user.Id);
            return View(exchangePrograms);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ExchangeProgram exchangeProgram)
        {
            return RedirectToAction("Index");
        }
        #endregion

    }
}
