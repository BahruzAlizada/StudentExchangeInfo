using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.UI.Controllers
{
    public class UniversityController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUniversityReadRepository universityReadRepository;
        public UniversityController(UserManager<AppUser> userManager, IUniversityReadRepository universityReadRepository)
        {
            this.userManager = userManager;
            this.universityReadRepository = universityReadRepository;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            int take = 6;
            List<University> universities = await universityReadRepository.GetActiveRegisteredTakedUniversitiesAsync(take);
            return View(universities);
        }
        #endregion
    }
}
