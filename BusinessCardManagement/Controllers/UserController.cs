using BusinessCardManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace BusinessCardManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        DBContext myDB = new DBContext();
        public ActionResult Index() {
            if(HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            var result = new Dictionary<string, string>();
            string passwordHash = Crypto.Hash(password, "MD5");
            User user = new User();
            user = (from users in myDB.users
                    where users.Email == email && users.Password == passwordHash
                    select users
                  ).FirstOrDefault();
            if (user != null)
            {
                
                FormsAuthentication.SetAuthCookie(user.UserID.ToString(), true);
                result.Add("result", "true");


            }
            else 
            {
                result.Add("result", "false");
            }
            
            return Json(result);


        }
    }
}