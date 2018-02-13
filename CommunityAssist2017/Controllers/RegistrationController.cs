using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017.Models;

namespace CommunityAssist2017.Controllers
{
    public class RegistrationController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        //NewPerson newPerson = new NewPerson();

        // GET: Registration
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Register([Bind(Include ="LastName, FirstName, Email, " +
                                                    "Password, ApartmentNumber, Street, " +
                                                    "City, State, ZipCode, " +
                                                    "Phone")] NewPerson p)
        {
            int result = db.usp_Register(p.LastName, p.FirstName, p.Email,
                                         p.Password, p.ApartmentNumber, p.Street,
                                         p.City, p.State, p.ZipCode,
                                         p.Phone);

            if(result != -1)
            {
                return RedirectToAction("Success");
            }

            return RedirectToAction("Failure");
        }

        public ActionResult Success()
        {
            Message successMsg =  new Message();
            successMsg.MessageText = "Thanks for registering.";
            return View("Result", successMsg);
        }

        public ActionResult Failure()
        {
            Message failureMsg = new Message();
            failureMsg.MessageText = "Sorry, but something seems to have gone wrong with the registration.";
            return View("Result", failureMsg);
        }
    }
}