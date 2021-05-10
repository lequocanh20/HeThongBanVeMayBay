using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public ActionResult Index()
        {
            return View(database.CATEGORies.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CATEGORY cate)
        {
            try
            {
                database.CATEGORies.Add(cate);
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
            return View(database.CATEGORies.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult Edit(int Id)
        {
            return View(database.CATEGORies.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, CATEGORY Cate)
        {
            database.Entry(Cate).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            return View(database.CATEGORies.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, CATEGORY Cate)
        {
            try
            {
                Cate = database.CATEGORies.Where(s => s.ID == Id).FirstOrDefault();
                database.CATEGORies.Remove(Cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Category");
            }
        }
    }
}