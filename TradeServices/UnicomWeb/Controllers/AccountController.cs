using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnicomWeb.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;
using UnicomWeb.Enums;

namespace UnicomWeb.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        // GET: Account
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Year = model.Year, LoginApproved = false };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "User");
                    return RedirectToAction("Login", "Account");
                    //return Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> SaveUserPassword(EditModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindByIdAsync(model.UserId);

                if (user != null)
                {
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ManageUsers", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь "+model.UserName+" не обнаружен");
                }
            }
            return View("EditUser", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else if (!user.LoginApproved)
                {
                    ModelState.AddModelError("", "Логин не подтвержден администратором");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize(Roles =  PortalRoles.Admin)]
        public ActionResult ManageUsers()
        {
             
            return View(UserManager.Users);
        }

        //[HttpPost]
        [Authorize]
        [Authorize(Roles = PortalRoles.Admin)]
        public async Task<JsonResult> DeleteUser(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    if (user.Id == User.Identity.GetUserId()) AuthenticationManager.SignOut();

                    return Json(true);
                }
            }
            return Json(false);
        }
        [Authorize]
        [Authorize(Roles = PortalRoles.Admin)]
        public async Task<JsonResult> ApproveLogin(string id, bool value)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.LoginApproved = value;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (!value && user.Id == User.Identity.GetUserId()) AuthenticationManager.SignOut();
                    return Json(true);
                }
            }
            return Json(false);
        }

        [Authorize]

        public async Task<ActionResult> EditUserPassword(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                EditModel editModel = new EditModel()
                {
                    UserName = user.UserName,
                    UserId = user.Id
                };
                return View("EditUser", editModel); 
            }
            else
                return HttpNotFound("Пользователь не обнаружен");
        }

        //[HttpPost]
        //[ActionName("Delete")]
        //[Authorize]
        //public async Task<ActionResult> DeleteConfirmed()
        //{
        //    //ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
        //    ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        IdentityResult result = await UserManager.DeleteAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Logout", "Account");
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
    }
}