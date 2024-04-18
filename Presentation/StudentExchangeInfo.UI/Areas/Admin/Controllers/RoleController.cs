using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Domain.Identity;
using System.Data;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
	public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> roleManager;
        public RoleController(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<AppRole> appRoles = await roleManager.Roles.ToListAsync();
            List<RoleVM> roleList = new List<RoleVM>();

            foreach (var item in appRoles)
            {
                RoleVM listVM = new RoleVM
                {
                    Id = item.Id,
                    RoleName = item.Name
                };
                roleList.Add(listVM);
            }
            return View(roleList);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(RoleVM role)
        {
            #region IsExist
            bool isExist = roleManager.Roles.ToList().Any(x => x.Name == role.RoleName);
            if (isExist)
            {
                ModelState.AddModelError("role", "Bu rol hal-hazırda zatən var");
                return View();
            }
            #endregion

            AppRole AppRole = new AppRole
            {
                Name = role.RoleName,
                NormalizedName = role.RoleName.ToUpper()
            };

            await roleManager.CreateAsync(AppRole);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            AppRole? dbRole = roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (dbRole == null) return BadRequest();

            RoleVM dbVM = new RoleVM
            {
                Id = dbRole.Id,
                RoleName = dbRole.Name,
            };

            return View(dbVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, RoleVM newRole)
        {
            #region Get
            if (id == null) return NotFound();
            AppRole? dbRole = roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (dbRole == null) return BadRequest();

            RoleVM dbVM = new RoleVM
            {
                Id = dbRole.Id,
                RoleName = dbRole.Name,
            };
            #endregion

            #region IsExist
            bool isExist = roleManager.Roles.ToList().Any(x => x.Name == newRole.RoleName && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("role", "Bu rol hal-hazırda zatən var");
                return View();
            }
            #endregion

            dbVM.Id = newRole.Id;
            dbVM.RoleName = newRole.RoleName;

            dbRole.Id = newRole.Id;
            dbRole.Name = newRole.RoleName;
            dbRole.NormalizedName = newRole.RoleName.ToUpper();

            return RedirectToAction("Index");
        }
        #endregion
    }  
}
