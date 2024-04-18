using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Application.ViewModels.VebUser;
using StudentExchangeInfo.Domain.Identity;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await userManager.Users.Where(x => x.UserRole.Contains("VBUSR")).ToListAsync();
            List<VebUserVM> vebUsersVM = new List<VebUserVM>();

            foreach (var item in users)
            {
                VebUserVM vm = new VebUserVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    SurName = item.Surname,
                    Email = item.Email,
                    Username = item.UserName,
                    Status = item.Status,
                    Created = item.Created,
                    Role = (await userManager.GetRolesAsync(item))[0]
                };
                vebUsersVM.Add(vm);
            }
            return View(vebUsersVM);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await roleManager.Roles.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(VebUserCreateVM vm, int roleId)
        {
            ViewBag.Roles = await roleManager.Roles.ToListAsync();
            AppRole? role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            if (role == null) return BadRequest();

            AppUser user = new AppUser
            {
                Name = vm.Name,
                Surname = vm.SurName,
                Email = vm.Email,
                UserName = vm.UserName,
                IsUser = false,
                Status = true,
                UserRole = "VBUSR"
            };

            IdentityResult result = await userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await userManager.AddToRoleAsync(user, role.Name);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            VebUserVM dbVM = new VebUserVM
            {
                Id = user.Id,
                Name = user.Name,
                SurName = user.Surname,
                Username = user.UserName,
                Email = user.Email,
                Created = user.Created,
                Status = user.Status
            };

            return View(dbVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, VebUserVM newVM)
        {
            #region Get
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            VebUserVM dbVM = new VebUserVM
            {
                Id = user.Id,
                Name = user.Name,
                SurName = user.Surname,
                Username = user.UserName,
                Email = user.Email,
                Created = user.Created,
                Status = user.Status
            };
            #endregion

          
            user.Id = newVM.Id;
            user.Name = newVM.Name;
            user.Surname = newVM.SurName;
            user.UserName = newVM.Username;
            user.Email = newVM.Email;
            user.Status = dbVM.Status;
            user.Created = dbVM.Created;

            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        #endregion

        #region RoleChange
        public async Task<IActionResult> RoleChange(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            ViewBag.Roles = await roleManager.Roles.Select(x => x.Name).ToListAsync();
            RoleVM roleVM = new RoleVM
            {RoleName = (await userManager.GetRolesAsync(user))[0]};

            return View(roleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> RoleChange(int? id, string role)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            ViewBag.Roles = await roleManager.Roles.Select(x => x.Name).ToListAsync();
            RoleVM roleVM = new RoleVM
            {RoleName = (await userManager.GetRolesAsync(user))[0]};

            await userManager.RemoveFromRoleAsync(user, roleVM.RoleName);
            await userManager.AddToRoleAsync(user, role);

            return RedirectToAction("Index");
        }
        #endregion

        #region ResetPassword
        public async Task<IActionResult> ResetPassword(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(int? id, ResetPasswordVM resetPassword)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            string token = await userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result = await userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null) return NotFound();
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            if (user.Status)
                user.Status = false;
            else
                user.Status = true;

            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
