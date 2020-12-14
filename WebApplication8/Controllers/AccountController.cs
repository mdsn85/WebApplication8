using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }


        ApplicationDbContext db = new ApplicationDbContext();


        [AllowAnonymous]
        public ActionResult RegisterMultiRoles()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name");

            ViewBag.Users = db.Roles.Select(u => new { label = u.Name, value = u.Name }).ToList();
            //ViewBag.RolesName = new SelectList(db.Roles.ToList(), "Id", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterMultiRoles(RegisterViewModel model )
        {
            if (ModelState.IsValid)
            {

                int eid = model.EmployeeId;
                Employee e = db.Employees.Find(eid);
                model.Email = e.EmailSignature;
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email ,IsEnabled =true};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");


                    //User role
                    foreach (string roleName in model.UserRolesList)
                    {
                        await this.UserManager.AddToRoleAsync(user.Id, roleName);
                    }


                    EmployeeUser eu = new EmployeeUser();
                    eu.User = user.UserName;
                    eu.EmployeeId = model.EmployeeId;
                    db.EmployeeUsers.Add(eu);
                    db.SaveChanges();


                    return RedirectToAction("Index", "Home");
                }
                ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name");
                ViewBag.Users = db.Roles.Select(u => new { label = u.Name, value = u.Name }).ToList();
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name");
            ViewBag.Users = db.Roles.Select(u => new { label = u.Name, value = u.Name }).ToList();
            return View(model);
        }





        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name");
            ViewBag.RolesName = new SelectList(db.Roles.ToList(), "Name", "Name");

            //ViewBag.RolesName = new SelectList(db.Roles.ToList(), "Id", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                int eid = model.EmployeeId;
                Employee e = db.Employees.Find(eid);
                model.Email = e.EmailSignature;
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");


                    //User role
                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    EmployeeUser eu = new EmployeeUser();
                    eu.User = user.UserName;
                    eu.EmployeeId = model.EmployeeId;
                    db.EmployeeUsers.Add(eu);
                    db.SaveChanges();


                    return RedirectToAction("Index", "Home");
                }
                ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name");
                ViewBag.RolesName = new SelectList(db.Roles.ToList(), "Name", "Name");
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name");
            ViewBag.RolesName = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }










        public ActionResult ListOfUsers(string searchStringName)
        {
            List<ListUsersViewModel> allusers = null;
            allusers = (from a in db.Users
                        select new ListUsersViewModel() { UserId = a.Id, UserName = a.UserName, Email = a.Email, IsEnabled = a.IsEnabled }).ToList();

            foreach (var u in allusers.ToList())
            {
                u.UserRolesList = UserManager.GetRoles(u.UserId).ToList();
            }
            return View(allusers.OrderBy(a => a.UserName).ToList());
        }

        public ActionResult EditRegisterMultiRoles(string id)
        {
            var user = db.Users.Where(u => u.UserName == id).FirstOrDefault();
            int empid = -1;
            try
            {
                empid = db.EmployeeUsers.Where(eu => eu.User == id).FirstOrDefault().EmployeeId;
            }
            catch (Exception e) { }
            string roleid = user.Roles.FirstOrDefault().RoleId;
            string userId = User.Identity.GetUserId();
            ViewBag.Users = db.Roles.Select(u => new { label = u.Name, value = u.Name }).ToList();
            ViewBag.CurrentRoles =UserManager.GetRoles(user.Id).ToList();
            // get user roles



            // ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name", empid);
            ViewBag.EmployeeName = db.Employees.Find(empid).Name;
            //User role

            RegisterViewModel model = new RegisterViewModel();
            model.idx = user.Id;
            model.Email = user.Email;
            model.IsEnabled = user.IsEnabled ?? false;
            model.UserName = user.UserName;

            model.EmployeeId = empid;
            model.Password = "";
            //if (User.IsInRole(RoleName.Admin))
            //{
            return View(model);
            //}
            //return HttpNotFound();
        }

        //
        // POST: /Account/Register
        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> EditRegisterMultiRoles(RegisterViewModel model)
        {

            var user = UserManager.FindById(model.idx);

            // Update it with the values from the view model
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.IsEnabled = model.IsEnabled;
            user.PasswordHash = user.PasswordHash;
            // var user = UserManager.FindByName(model.UserName);

            //var EmployeeUser = db.EmployeeUsers.Where(ee => ee.User == user.UserName).FirstOrDefault();


            try
            {
                UserManager.Update(user);

                //await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                var roles = await UserManager.GetRolesAsync(user.Id);
                await UserManager.RemoveFromRolesAsync(user.Id, roles.ToArray());
                if (model.UserRolesList != null)
                {
                    foreach (string roleName in model.UserRolesList)
                    {
                        await this.UserManager.AddToRoleAsync(user.Id, roleName);
                    }
                }


                EmployeeUser eu = db.EmployeeUsers.Where(ee => ee.EmployeeId == model.EmployeeId).FirstOrDefault();
                eu.User = user.UserName;
                eu.EmployeeId = model.EmployeeId;
               
                db.SaveChanges();
                return RedirectToAction("ListOfUsers", "Account");
            }
            catch (Exception e)
            {
                ViewBag.e = e.Message;

                ViewBag.Users = db.Roles.Select(u => new { label = u.Name, value = u.Name }).ToList();
                //ViewBag.EmployeeName = 
                return View(model);
            }

           
        }


        public ActionResult EditRegister(string id)
        {
            var user = db.Users.Where(u => u.UserName == id).FirstOrDefault();
            int empid = -1;
            try
            {
                empid = db.EmployeeUsers.Where(eu => eu.User == id).FirstOrDefault().EmployeeId;
            }
            catch (Exception e) { }
            string roleid = user.Roles.FirstOrDefault().RoleId;
            string userId = User.Identity.GetUserId();

            string roleName = db.Roles.Find(roleid).Name;
            // get user roles



            ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name", empid);

            //User role
            ViewBag.RolesName = new SelectList(db.Roles.ToList(), "Name", "Name", roleName);
            RegisterViewModel model = new RegisterViewModel();
            model.idx = user.Id;
            model.Email = user.Email;
            model.IsEnabled = user.IsEnabled ?? false;
            model.UserName = user.UserName;
            model.UserRoles = roleName;
            model.EmployeeId = empid;
            model.Password = "";
            //if (User.IsInRole(RoleName.Admin))
            //{
            return View(model);
            //}
            //return HttpNotFound();
        }

        //
        // POST: /Account/Register
        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> EditRegister(RegisterViewModel model)
        {
           // var user = UserManager.FindByName(model.UserName);
            var user = db.Users.Where(y => y.Id == model.idx).FirstOrDefault();
            // Update it with the values from the view model
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.IsEnabled = model.IsEnabled;

            var EmployeeUser = db.EmployeeUsers.Where(ee => ee.User == user.UserName).FirstOrDefault();

            //user.PasswordHash = user.PasswordHash;
            // Apply the changes if any to the db
            try
            {
                UserManager.Update(user);

                await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);

                //var RoleRegisterUser = new RoleStore<IdentityRole>(new ApplicationDbContext());
                //var roleManager = new RoleManager<IdentityRole>(RoleRegisterUser);
                //await roleManager.CreateAsync(new IdentityRole("canManageUser"));
                //await UserManager.AddToRoleAsync(user.Id, "canManageUser");



                //     await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");




                //User role
                //await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);


                //if employee has not assign to user yet
                if (EmployeeUser == null)
                {
                    EmployeeUser eu = new EmployeeUser();
                    eu.User = user.UserName;
                    eu.EmployeeId = model.EmployeeId;
                    db.EmployeeUsers.Add(eu);
                    db.SaveChanges();
                }
                else
                {
                    EmployeeUser.User = user.UserName;
                    EmployeeUser.EmployeeId = model.EmployeeId;
                    db.SaveChanges();
                }

                return RedirectToAction("ListOfUsers", "Account");
            }
            catch (Exception e)
            {
                ViewBag.e = e.Message;
                ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name");
                //User role
                ViewBag.RolesName = new SelectList(db.Roles.Where(u => !u.Name.Contains("Admin"))
                                                .ToList(), "Name", "Name");

                //User role
                ViewBag.Name = new SelectList(db.Roles.Where(u => !u.Name.Contains("Admin"))
                          .ToList(), "Name", "Name");
                return View(model);
            }

            //var user1 = db.Users.Where(u => u.UserName == model.UserName).FirstOrDefault();
            //int empid = -1;
            //try
            //{
            //    empid = db.EmployeeUsers.Where(eu => eu.User == user1.UserName).FirstOrDefault().EmployeeId;
            //}
            //catch (Exception e) {
            //    string roleid = user1.Roles.FirstOrDefault().RoleId;
            //    string userId = User.Identity.GetUserId();

            //    string roleName = db.Roles.Find(roleid).Name;
            //    // get user roles



            //    ViewBag.EmployeeId = new SelectList(db.employees.OrderBy(s => s.Name), "EmployeeId", "Name", empid);

            //    //User role
            //    ViewBag.RolesName = new SelectList(db.Roles.Where(u => !u.Name.Contains("Admin"))
            //                                    .ToList(), "Name", "Name", roleName);

            //    // If we got this far, something failed, redisplay form
            //    return View(model);
            //}

        }

        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult ResetPasswordCust(string id)
        {
            var user = db.Users.Where(u => u.UserName == id).FirstOrDefault();
            int empid = -1;
            try
            {
                empid = db.EmployeeUsers.Where(eu => eu.User == id).FirstOrDefault().EmployeeId;
            }
            catch (Exception e) { }
            string roleid = user.Roles.FirstOrDefault().RoleId;
            string userId = User.Identity.GetUserId();

            string roleName = db.Roles.Find(roleid).Name;
            // get user roles



            ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name", empid);

            //User role
            ViewBag.RolesName = new SelectList(db.Roles.Where(u => !u.Name.Contains("Admin"))
                                            .ToList(), "Name", "Name", roleName);
            RegisterViewModel model = new RegisterViewModel();
            model.idx = user.Id;
            model.Email = user.Email;
            model.IsEnabled = user.IsEnabled ?? false;
            model.UserName = user.UserName;
            model.UserRoles = roleName;
            model.EmployeeId = empid;
            model.Password = user.PasswordHash;
            //if (User.IsInRole(RoleName.Admin))
            if (empid != -1)
            {
                ViewBag.empname = db.Employees.Find(empid).Name;
            }
            else
            {
                ViewBag.empname = "";
            }
            return View(model);
            //}
            //return HttpNotFound();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        public async Task<ActionResult> ResetPasswordCust(RegisterViewModel model)
        {
            // Apply the changes if any to the db
            try
            {

                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());

                userManager.RemovePassword(model.idx);

                userManager.AddPassword(model.idx, model.Password);
                return RedirectToAction("ListOfUsers", "Account");
            }
            catch (Exception e)
            {
                ViewBag.e = e.Message;
                ViewBag.EmployeeId = new SelectList(db.Employees.OrderBy(s => s.Name), "EmployeeId", "Name");
                //User role
                ViewBag.RolesName = new SelectList(db.Roles.Where(u => !u.Name.Contains("Admin"))
                                                .ToList(), "Name", "Name");

                //User role
                ViewBag.Name = new SelectList(db.Roles.Where(u => !u.Name.Contains("Admin"))
                          .ToList(), "Name", "Name");
                return View(model);
            }


        }

        [HttpGet]

        public ActionResult RoleCreate()

        {

            return View(new Role());

        }



        [HttpPost]

        public ActionResult RoleCreate(Role role)

        {

            if (ModelState.IsValid)

            {

                if (Roles.RoleExists(role.RoleName))

                {

                    ModelState.AddModelError("Error", "Rolename already exists");

                    return View(role);

                }

                else

                {

                    Roles.CreateRole(role.RoleName);

                    return RedirectToAction("RoleIndex", "Account");

                }

            }

            else

            {

                ModelState.AddModelError("Error", "Please enter Username and Password");

            }

            return View(role);

        }
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }


}