using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WA_Blogers_MVC.Models;
namespace WA_Blogers_MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        [NonAction]
        private UserStatus GetUSerValidation(WA_Users u){
            if (u != null)
            {
                if (u.IsAdmin == true)
                    return UserStatus.AuthenticationAdmin;
                else return UserStatus.AuthenticationGuest;
            }
            else return UserStatus.NonAuthentication;
        }
        //
        // GET: /Authentication/
        public ActionResult Index()
        {
            return View();
        }

        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {

        }

	}
}