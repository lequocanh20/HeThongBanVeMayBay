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
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
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
        public ActionResult Search(DateTime? DepartDate, DateTime? ReturnDate)
        {
            //if(ReturnDate == null)
            //{
            //    //var list = database.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingFrom + "' AND IDSanBayDen = '" + FlyingTo + "' AND NgayGio = '" + DepartDate.Value.ToString("yyyy/MM/dd") + "'").ToList();
            //    //return View(list);
            //    return RedirectToAction("OneWay", "BookTicket");
            //}
            //else
            //{
            //    //var list = database.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingFrom + "' AND IDSanBayDen = '" + FlyingTo + "' AND NgayGio = '" + DepartDate.Value.ToString("yyyy/MM/dd") + "'").ToList();
            //    //var list1 = database.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingTo + "' AND IDSanBayDen = '" + FlyingFrom + "' AND NgayGio = '" + ReturnDate.Value.ToString("yyyy/MM/dd") + "'").ToList();
            //    //return View(list);
            //}
            ViewData["DepartDate"] = DepartDate;
            ViewData["ReturnDate"] = ReturnDate;
            return View();
        }
        public static List<CHUYENBAY> OneWay(string FlyingFrom, string FlyingTo, DateTime? DepartDate)
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingFrom + "' AND IDSanBayDen = '" + FlyingTo + "' AND NgayGio = '" + DepartDate.Value.ToString("yyyy/MM/dd") + "'").ToList())
                {
                    rtn.Add(new CHUYENBAY
                    {
                        ID = item.ID,
                        IDChuyenBay = item.IDChuyenBay,
                        HangBay = item.HANGBAY1.TenHangbay,
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
        public static List<CHUYENBAY> TwoWay(string FlyingFrom, string FlyingTo, DateTime? ReturnDate)
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingTo + "' AND IDSanBayDen = '" + FlyingFrom + "' AND NgayGio = '" + ReturnDate.Value.ToString("yyyy/MM/dd") + "'").ToList())
                {
                    rtn.Add(new CHUYENBAY
                    {
                        ID = item.ID,
                        IDChuyenBay = item.IDChuyenBay,
                        HangBay = item.HANGBAY1.TenHangbay,
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

        public PartialViewResult RenderOneWay(string FlyingFrom, string FlyingTo, DateTime? DepartDate, DateTime? ReturnDate)
        {
            return PartialView(OneWay(FlyingFrom, FlyingTo, DepartDate));
        }
        public PartialViewResult RenderTwoWay(string FlyingFrom, string FlyingTo, DateTime? DepartDate, DateTime? ReturnDate)
        {
            return PartialView(TwoWay(FlyingFrom, FlyingTo, ReturnDate));              
        }
    }
}