using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet4_sample1.Controllers
{
    public class AccountController : Controller
    {

        [AllowAnonymous]
        // GET: Account
        public ActionResult Login(string returnUrl)
        {

            if (string.IsNullOrEmpty(returnUrl) || !this.Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }

            // you can use this for the 'authParams.state' parameter
            // in Lock, to provide a return URL after the authentication flow.
            ViewBag.State = HttpUtility.UrlEncode(returnUrl);

            return this.View();
        }

        public ActionResult Logout()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            var returnTo = Url.Action("Index", "Home", null, protocol: Request.Url.Scheme);
            return this.Redirect(
        string.Format(CultureInfo.InvariantCulture,
            "https://{0}/v2/logout?federated&returnTo={1}&client_id={2}",
            ConfigurationManager.AppSettings["auth0:Domain"],
            this.Server.UrlEncode(returnTo),
            ConfigurationManager.AppSettings["auth0:ClientId"]));
        }
    }

    

    
}