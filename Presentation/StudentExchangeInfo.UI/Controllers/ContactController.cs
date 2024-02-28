using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactWriteRepository contactWriteRepository;
        public ContactController(IContactWriteRepository contactWriteRepository)
        {
           this.contactWriteRepository=contactWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Index(Contact contact)
        {
            await contactWriteRepository.AddAsync(contact);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
