using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017.Models;

namespace CommunityAssist2017.Controllers
{
    public class NewGrantController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: NewGrant
        public ActionResult Index()
        {
            if (Session["personKey"] == null)
            {
                Message m = new Message();
                m.MessageText = "Must be logged in to apply!";
                return RedirectToAction("Result", m);
            }

            ViewBag.GrantTypeKey = new SelectList(db.GrantTypes, "GrantTypeKey", "GrantTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include ="GrantTypeKey, GrantApplicationRequestAmount, GrantApplicationReason")] NewGrant ng)
        {
            GrantApplication grant = new GrantApplication();
            grant.PersonKey = (int)Session["personKey"];
            grant.GrantAppicationDate = DateTime.Now;
            grant.GrantTypeKey = ng.GrantTypeKey;
            grant.GrantApplicationRequestAmount = ng.GrantApplicationRequestAmount;
            grant.GrantApplicationReason = ng.GrantApplicationReason;
            db.GrantApplications.Add(grant);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "Thank you for applying! We will get back to you shortly.";

            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}