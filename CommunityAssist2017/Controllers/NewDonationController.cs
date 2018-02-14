using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017.Models;

namespace CommunityAssist2017.Controllers
{
    public class NewDonationController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();

        // GET: NewDonation
        public ActionResult Index()
        {
            if(Session["personKey"] == null)
            {
                Message m = new Message();
                m.MessageText = "Must be logged in to donate!";
                return RedirectToAction("Result", m);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include = "Amount")] NewDonation nd)
        {
            Donation d = new Donation();
            d.DonationAmount = nd.Amount;
            d.DonationDate = DateTime.Now;
            d.PersonKey = (int)Session["personKey"];
            d.DonationConfirmationCode = Guid.NewGuid();
            db.Donations.Add(d);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "Thank you for your donation!";

            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}