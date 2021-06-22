using HeThongBanVeMayBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongBanVeMayBay.Controllers
{
    public class HomePage1Controller : Controller
    {
        // GET: HomePage1
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public static List<SANBAY> SelectAllArticle()
        {
            var rtn = new List<SANBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.SANBAYs.Where(s => s.Status == "Active"))
                {
                    rtn.Add(new SANBAY
                    {
                        ID = item.ID,
                        //IDSanBay = item.IDSanBay,
                        TenSB = item.TenSB
                    });
                }
            }
            return rtn;
        }
        public List<HangVe> GetAllCategories()
        {
            var hangVes = new List<HangVe>
            {
                new HangVe {Id = 1, Name = "Hạng thương gia"},
                new HangVe {Id = 2, Name = "Hạng phổ thông"},
            };

            return hangVes.ToList();
        }
        public ActionResult Index()
        {
            ViewBag.HangVeList = new SelectList(GetAllCategories(), "Id", "Name");
            List<SANBAY> list = SelectAllArticle().ToList();
            ViewBag.listSanBay = new SelectList(list, "IATA", "TenSB", 1);
            return View();
        }
    }
}