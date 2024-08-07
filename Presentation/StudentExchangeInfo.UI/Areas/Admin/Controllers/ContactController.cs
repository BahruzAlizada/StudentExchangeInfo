﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin,ContactManager")]
	public class ContactController : Controller
    {
        private readonly IContactReadRepository contactReadRepository;
        private readonly IContactWriteRepository contactWriteRepository;
        public ContactController(IContactReadRepository contactReadRepository, IContactWriteRepository contactWriteRepository)
        {
            this.contactReadRepository = contactReadRepository;
            this.contactWriteRepository = contactWriteRepository;
        }

        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.PageCount = await contactReadRepository.ContactPageCountAsync(24);
            ViewBag.CurrentPage = page;

            List<Contact> contacts = await contactReadRepository.GetContactsWithPagedAsync(24, page);
            return View(contacts);
        }
        #endregion

        #region Detail
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = contactReadRepository.Get(x => x.Id == id);
            if (contact == null) return BadRequest();

            return View(contact);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = contactReadRepository.Get(x => x.Id == id);
            if (contact == null) return BadRequest();

            contactWriteRepository.Delete(contact);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
