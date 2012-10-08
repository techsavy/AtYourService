
namespace AtYourService.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Core.Geo;
    using Domain.Users;
    using Helpers;
    using Models;
    using Properties;
    using Security;
    using Util;

    public class AccountsController : BaseController
    {
        private readonly IFormsAuthenticationService _formsAuthenticationService;

        public AccountsController(NHibernateContext nHibernateContext, IFormsAuthenticationService formsAuthenticationService)
            : base(nHibernateContext)
        {
            _formsAuthenticationService = formsAuthenticationService;
        }

        //
        // GET: /Accounts/SignUp

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel signUp)
        {
            if (ModelState.IsValid)
            {
                var createClient = new CreateClientCommand(signUp.Name, signUp.Email, signUp.Password, signUp.Brag, 
                    signUp.Latitude, signUp.Longitude, signUp.Source.Value);

                ExecuteCommand(createClient);

                return RedirectToAction("SignUpSuccess");
            }

            return View();
        }

        public ActionResult SignUpSuccess()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = ExecuteCommand(new LoginUserCommand(model.Email, model.Password));
                if (user != null && !user.IsLocked)
                {
                    var userInfo = Mapper.Map<UserInfo>(user);
                    Session[SessionKeys.User] = userInfo;

                    _formsAuthenticationService.SignIn(model.Email, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (user != null && user.IsLocked)
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOut()
        {
            _formsAuthenticationService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [ActionName("Profile")]
        [Authorize]
        public ActionResult MyProfile()
        {
            var user = ExecuteQuery(session => session.Load<User>(UserInfo.Id));

            return View(user);
        }

        [Authorize]
        public ActionResult Edit()
        {
            var user = ExecuteQuery(session => session.Load<User>(UserInfo.Id));

            var model = Mapper.Map<EditUserModel>(user);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(EditUserModel model)
        {
            var command = new EditProfileCommand(UserInfo.Id, model.Brag, model.Latitude, model.Longitude);

            ExecuteCommand(command);

            TempData[ViewDataKeys.Message] = new SuccessMessage(Resources.ProfileUpdateSuccess);

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public ActionResult UpdateLocation(double lat, double lng)
        {
            if (Session[SessionKeys.User] != null)
            {
                var userInfo = (UserInfo) Session[SessionKeys.User];
                userInfo.Location = PointFactory.Create(lat, lng);
            }
            else
            {
                var userInfo = new UserInfo { IsAdmin = false, IsAuthenticated = false, Location = PointFactory.Create(lat, lng) };
                Session[SessionKeys.User] = userInfo;
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public JsonResult ChangePassword(ChangePasswordViewModel changePasswordView)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Success = false, Message = Resources.ValidationFailed });
            }

            var command = new ChangePasswordCommand(UserInfo.Id, changePasswordView.CurrentPassword, changePasswordView.NewPassword);
            var result = ExecuteCommand(command);

            if (result)
            {
                TempData[ViewDataKeys.Message] = new SuccessMessage(Resources.PasswordChangeSuccess);

                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false, Message = Resources.IncorrectCurrentPassword });
            }
        }
    }
}
