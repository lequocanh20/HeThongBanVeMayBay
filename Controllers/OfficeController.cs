using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    [Authorize(Roles = "true")]
    public class OfficeController : Controller
    {
        // GET: Office
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public ActionResult Index()
        {
            return View(database.CHUCVUs.ToList());
        }
        public ActionResult Create()
        {
            List<string> office = new List<string>(3);
            office.Add("true");
            office.Add("false");
            office.Add("diffe");
            ViewBag.listAdmin = new SelectList(office);
            CHUCVU cv = new CHUCVU();
            return View(cv);
        }

        [HttpPost]
        public ActionResult Create(CHUCVU Chucvu)
        {           
            try
            {
                database.CHUCVUs.Add(Chucvu);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        public ActionResult Details(int Id)
        {
            return View(database.CHUCVUs.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult Edit(int Id)
        {
            List<string> office = new List<string>(3);
            office.Add("true");
            office.Add("false");
            office.Add("diffe");
            ViewBag.listAdmin = new SelectList(office);
            return View(database.CHUCVUs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, CHUCVU Chucvu)
        {
            List<string> office = new List<string>(3);
            office.Add("true");
            office.Add("false");
            office.Add("diffe");         
            database.Entry(Chucvu).State = System.Data.Entity.EntityState.Modified;
            ViewBag.listAdmin = new SelectList(office);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}