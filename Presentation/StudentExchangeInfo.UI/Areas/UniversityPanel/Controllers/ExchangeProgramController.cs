using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.UI.Areas.UniversityPanel.Controllers
{
    [Area("UniversityPanel")]
	[Authorize(Roles = "University")]
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

            List<ExchangeProgram> exchangePrograms = await exchangeProgramReadRepository.GetExchangeProgramsByUniversityAsync(user.Id);
            return View(exchangePrograms);
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            ExchangeProgram? exchangeProgram = await exchangeProgramReadRepository.GetAsync(x => x.Id == id); ;
            if (exchangeProgram == null) return BadRequest();

            return View(exchangeProgram);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if(user == null) return BadRequest();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ExchangeProgram exchangeProgram)
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            bool result = exchangeProgramReadRepository.GetAll().Any(x => x.ExchangeName == exchangeProgram.ExchangeName);
            if (result)
            {
                ModelState.AddModelError("ExchangeName", "Bu adda müadilə proqramı artıq mövcuddur");
                return View();
            }

            exchangeProgram.AppUserId = user.Id;

            exchangeProgramWriteRepository.Add(exchangeProgram);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            ExchangeProgram? dbExchangeProgram = exchangeProgramReadRepository.Get(x => x.Id == id);
            if (dbExchangeProgram == null) return BadRequest();

            return View(dbExchangeProgram);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id,ExchangeProgram exchangeProgram)
        {
            if (id == null) return NotFound();
            ExchangeProgram? dbExchangeProgram = exchangeProgramReadRepository.Get(x => x.Id == id);
            if (dbExchangeProgram == null) return BadRequest();

            bool result = exchangeProgramReadRepository.GetAll().Any(x => x.ExchangeName == exchangeProgram.ExchangeName && x.Id!=id);
            if (result)
            {
                ModelState.AddModelError("ExchangeName", "Bu adda müadilə proqramı artıq mövcuddur");
                return View();
            }

            dbExchangeProgram.Id = exchangeProgram.Id;
            exchangeProgram.AppUserId = dbExchangeProgram.AppUserId;
            dbExchangeProgram.ExchangeName = exchangeProgram.ExchangeName;
            dbExchangeProgram.Description = exchangeProgram.Description;
            dbExchangeProgram.Condition = exchangeProgram.Condition;
            dbExchangeProgram.Document = exchangeProgram.Document;
            dbExchangeProgram.Status = exchangeProgram.Status;

            exchangeProgramWriteRepository.Update(exchangeProgram);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int? id)
        {
            if (id == null) return NotFound();
            ExchangeProgram? exchangeProgram = exchangeProgramReadRepository.Get(x => x.Id == id);
            if (exchangeProgram == null) return BadRequest();

            exchangeProgramWriteRepository.Activity(exchangeProgram);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
