using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.UI.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class ContactController : Controller
    {
        private readonly IContactWriteRepository contactWriteRepository;
        private readonly UserManager<AppUser> userManager;
        public ContactController(IContactWriteRepository contactWriteRepository, UserManager<AppUser> userManager)
        {
            this.contactWriteRepository = contactWriteRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Index(Contact contact)
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            contact.FullName = user.Name + user.Surname;
            contact.Email = user.Email;

            await contactWriteRepository.AddAsync(contact);
            return RedirectToAction("Index");
        }
    }
}
