using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WA_Blogers_MVC.Models;
namespace WA_Blogers_MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        [NonAction]
        private UserStatus GetUSerValidation(WA_Users u){
            if (u.IsAdmin == true)
                return UserStatus.AuthenticationAdmin;
            else return UserStatus.AuthenticationGuest;
        }
        [NonAction]
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // GET: Authentication
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName,Password")] WA_Users wa_User)
        {
            if (ModelState.IsValid)
            {WA_BlogerEntities db = new WA_BlogerEntities();
                MD5 md5 = MD5.Create();
                string passHash = GetMd5Hash(md5, wa_User.Password);
            if (db.WA_Users.Count() == 0)
            {
                wa_User.Password = passHash;
                wa_User.DisplayName = "Quản trị viên";
                wa_User.Created = DateTime.Now;
                wa_User.IsAdmin = true;
                wa_User.Email = "default@example.com";
                db.WA_Users.Add(wa_User);
                db.SaveChanges();
            }
                
                WA_Users user_Login = db.WA_Users.Where(x => x.UserName == wa_User.UserName &&
                    x.Password == passHash).FirstOrDefault();
                if (user_Login == null)
                {
                    ViewBag.LoginFailer = "Tài khoản hoặc mật khẩu không đúng";
                    ModelState.AddModelError("LoginFailer", "Tài khoản hoặc mật khẩu không đúng");
                    return View();
                }
                else
                {
                    UserStatus userStatus = GetUSerValidation(user_Login);
                    bool IsAdmin = false;
                    switch (userStatus)
                    {
                        case UserStatus.AuthenticationAdmin:
                            IsAdmin = true;
                        break;
                        case  UserStatus.AuthenticationGuest:
                            IsAdmin = false;
                        break;
                    }
                    FormsAuthentication.SetAuthCookie(user_Login.UserName,false);
                    user_Login.LastLogin = DateTime.Now;
                    db.Entry(user_Login).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["IsAdmin"] = IsAdmin;
                    Session["UserLogin"] = user_Login;
                    return RedirectToAction("Index", "Admin");

                }
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

	}
}