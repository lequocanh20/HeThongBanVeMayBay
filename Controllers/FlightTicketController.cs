using HeThongBanVeMayBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongBanVeMayBay.Controllers
{
    [Authorize(Roles = "true, false")]
    public class FlightTicketController : Controller
    {
        // GET: FlightTicket
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public ActionResult Index()
        {
            return View(database.VECHUYENBAYs.ToList());
        }

        public ActionResult Cancle(string madatcho)
        {
            using (var context = new QLBANVEMAYBAYEntities())
            {
                context.Database.ExecuteSqlCommand("UPDATE VECHUYENBAY SET Status = 'Cancle' WHERE IDVeChuyenBay = '" + madatcho + "'");
            }
            return RedirectToAction("Index", "FlightTicket");
        }

        public ActionResult Details(string id)
        {
            return View(database.VECHUYENBAYs.Where(s => s.IDVeChuyenBay == id).FirstOrDefault());
        }
    }
}