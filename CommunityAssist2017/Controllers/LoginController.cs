using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017.Models;

namespace CommunityAssist2017.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]                      
        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include ="UserName, Password")] LoginClass login)
        {
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            int loginResult = db.usp_Login(login.UserName, login.Password);

            if(loginResult != -1)
            {
                var uid = (from r in db.People
                          where r.PersonEmail.Equals(login.UserName)
                          select r.PersonKey).FirstOrDefault();

                int personKey = (int)uid;
                Session["personKey"] = personKey;

                Message loginOkay = new Message();
                loginOkay.MessageText = "Thank you, " + login.UserName + " login successful!";
                return RedirectToAction("Result", loginOkay);
            }

            Message loginFail = new Message();
            loginFail.MessageText = "Invalid credentials! Try again or register.";
            return View("Result", loginFail);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}