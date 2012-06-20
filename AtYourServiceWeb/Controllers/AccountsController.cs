
namespace AtYourService.Web.Controllers
{
    using System.Web.Mvc;
    using Domain.Users;
    using Models;
    using Security;
    using AutoMapper;
    using NHibernate;

    public class AccountsController : BaseController
    {
        private readonly IFormsAuthenticationService _formsAuthenticationService;

        public AccountsController(ISession session, IFormsAuthenticationService formsAuthenticationService) : base(session)
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

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = ExecuteCommand(new LoginUserCommand(model.Email, model.Password));
                if (user != null)
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
        public ActionResult MyProfile()
        {
            return View();
        }
    }
}
