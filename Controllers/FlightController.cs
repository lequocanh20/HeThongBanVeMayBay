using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    public class FlightController : Controller
    {
        // GET: Flight
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public static List<CHUYENBAY> SelectAllArticle()
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs)
                {
                    rtn.Add(new CHUYENBAY
                    {
                        ID = item.ID,
                        IDChuyenBay = item.IDChuyenBay,
                        IDSanBayDen = item.SANBAY1.TenSB,
                        IDSanBayDi = item.SANBAY.TenSB,
                        GiaTien = item.GiaTien,
                        NgayGio = item.NgayGio.Date,
                        ThoiGianBay = item.ThoiGianBay,
                        SoGheHang1 = item.SoGheHang1,
                        SoGheHang2 = item.SoGheHang2
                    });
                }
            }
            return rtn;
        }
        public static List<SANBAY> SelectAllArticle1()
        {
            var rtn = new List<SANBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.SANBAYs)
                {
                    rtn.Add(new SANBAY
                    {
                        IDSanBay = item.IDSanBay,
                        TenSB = item.TenSB
                    });
                }
            }
            return rtn;
        }
        public ActionResult Index()
        {
            return View(SelectAllArticle().ToList());
        }
        public ActionResult Create()
        {
            List<SANBAY> list = SelectAllArticle1().ToList();
            ViewBag.listSanBay = new SelectList(list, "IDSanBay", "TenSB", "");
            CHUYENBAY cb = new CHUYENBAY();
            return View(cb);
        }
        [HttpPost]
        public ActionResult Create(CHUYENBAY cb)
        {
            List<SANBAY> list = SelectAllArticle1().ToList();
            try
            {
                ViewBag.listSanBay = new SelectList(list, "IDSanBay", "TenSB", 1);
                database.CHUYENBAYs.Add(cb);
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
            return View(database.CHUYENBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult Edit(int Id)
        {
            List<SANBAY> list = SelectAllArticle1().ToList();
            ViewBag.listSanBay = new SelectList(list, "IDSanBay", "TenSB", "");
            return View(database.CHUYENBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, CHUYENBAY chuyenbay)
        {
            List<SANBAY> list = SelectAllArticle1().ToList();
            ViewBag.listSanBay = new SelectList(list, "IDSanBay", "TenSB", 1);
            database.Entry(chuyenbay).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            return View(database.CHUYENBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, CHUYENBAY chuyenbay)
        {
            try
            {
                chuyenbay = database.CHUYENBAYs.Where(s => s.ID == Id).FirstOrDefault();
                database.CHUYENBAYs.Remove(chuyenbay);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Flight");
            }
        }
    }
}