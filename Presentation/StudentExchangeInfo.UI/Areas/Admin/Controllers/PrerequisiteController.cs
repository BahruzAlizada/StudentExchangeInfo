using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PrerequisiteController : Controller
    {
        private readonly IPrerequisiteReadRepository prerequisiteReadRepository;
        private readonly IPrerequisiteWriteRepository prerequisiteWriteRepository;
        public PrerequisiteController(IPrerequisiteReadRepository prerequisiteReadRepository, IPrerequisiteWriteRepository prerequisiteWriteRepository)
        {
            this.prerequisiteReadRepository = prerequisiteReadRepository;
            this.prerequisiteWriteRepository = prerequisiteWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            Prerequisite prerequisite = prerequisiteReadRepository.Get();
            return View(prerequisite);
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Prerequisite dbPre = prerequisiteReadRepository.Get(x => x.Id == id);
            if (dbPre == null) return BadRequest();

            return View(dbPre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, Prerequisite pre)
        {
            if (id == null) return NotFound();
            Prerequisite dbPre = prerequisiteReadRepository.Get(x => x.Id == id);
            if (dbPre == null) return BadRequest();

            dbPre.Id = pre.Id;
            dbPre.Status = pre.Status;
            dbPre.Description = pre.Description;

            prerequisiteWriteRepository.Update(pre);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
