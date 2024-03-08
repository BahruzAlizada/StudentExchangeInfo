using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.EntityFramework;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MotivationController : Controller
    {
        private readonly IMotivationReadRepository motivationReadRepository;
        private readonly IMotivationWriteRepository motivationWriteRepository;
        public MotivationController(IMotivationReadRepository motivationReadRepository, IMotivationWriteRepository motivationWriteRepository)
        {
            this.motivationReadRepository = motivationReadRepository;
            this.motivationWriteRepository = motivationWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            Motivation motivation = motivationReadRepository.Get();
            return View(motivation);
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Motivation dbMotivation = motivationReadRepository.Get(x => x.Id == id);
            if (dbMotivation == null) return BadRequest();

            return View(dbMotivation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, Motivation motivation)
        {
            if (id == null) return NotFound();
            Motivation dbMotivation = motivationReadRepository.Get(x => x.Id == id);
            if (dbMotivation == null) return BadRequest();

            dbMotivation.Id = motivation.Id;
            dbMotivation.Status = motivation.Status;
            dbMotivation.MotivationENG = motivation.MotivationENG;
            dbMotivation.MotivationAZE = motivation.MotivationAZE;

            motivationWriteRepository.Update(motivation);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
