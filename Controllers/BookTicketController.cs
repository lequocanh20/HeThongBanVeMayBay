using HeThongBanVeMayBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongBanVeMayBay.Controllers
{
    public class BookTicketController : Controller
    {
        // GET: BookTickett
        public static List<SANBAY> SelectAllArticle()
        {
            var rtn = new List<SANBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.SANBAYs)
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
        public ActionResult Index()
        {
            List<SANBAY> list = SelectAllArticle().ToList();
            ViewBag.listSanBay = new SelectList(list, "IATA", "TenSB", 1);
            return View();
        }       
    }
}