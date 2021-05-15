using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    public class AircraftController : Controller
    {
        // GET: Aircraft
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public ActionResult Index()
        {
            return View(database.MAYBAYs.ToList());
        }
        public ActionResult Create()
        {
            MAYBAY mb = new MAYBAY();
            return View(mb);
        }
        [HttpPost]
        public ActionResult Create(MAYBAY mb)
        {
            try
            {
                database.MAYBAYs.Add(mb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        public ActionResult Edit(int Id)
        {
            return View(database.MAYBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, MAYBAY mb)
        {
            database.Entry(mb).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int Id)
        {
            return View(database.MAYBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult Delete(int Id)
        {
            return View(database.MAYBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, MAYBAY mb)
        {
            try
            {
                mb = database.MAYBAYs.Where(s => s.ID == Id).FirstOrDefault();
                database.MAYBAYs.Remove(mb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Aircraft");
            }
        }
    }
}