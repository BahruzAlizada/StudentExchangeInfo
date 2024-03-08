using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Controllers
{
    public class PrerequisiteController : Controller
    {
        private readonly IPrerequisiteReadRepository prerequisiteReadRepository;
        public PrerequisiteController(IPrerequisiteReadRepository prerequisiteReadRepository)
        {
            this.prerequisiteReadRepository = prerequisiteReadRepository;
        }

        public IActionResult Index()
        {
            Prerequisite prerequisite = prerequisiteReadRepository.Get(); 
            return View(prerequisite);
        }
    }
}
