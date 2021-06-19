using BusinessCardManagement.Models;
using Microsoft.Ajax.Utilities;
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
        public ActionResult Index() {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            DBContext myDB = new DBContext();
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
        public ActionResult SginUp() {
            return View();
        }
        [HttpPost]
        public ActionResult SginUp(string email, string password, string username)
        {
            DBContext myDB = new DBContext();
            var result = new Dictionary<string, string>();
            User userCheck = CheckEmail(email);
            if (userCheck== null) {
                string passwordHash = Crypto.Hash(password, "MD5");

                User user = new User();
                user.Email = email;
                user.Password = passwordHash;
                user.UserName = username;
                myDB.users.Add(user);
                myDB.SaveChanges();

                result.Add("result", "true");

            }
            else
            {
                result.Add("result", "false");
                result.Add("msg", "This email already exists");

            }
            return Json(result);
        }
        public User CheckEmail(string email)
        {
            DBContext myDB = new DBContext();
            User user = new User();
            user = (from users in myDB.users
                    where users.Email == email 
                    select users
                  ).FirstOrDefault();
            return user;
        }

    }
   
}