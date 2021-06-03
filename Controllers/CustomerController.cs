using HeThongBanVeMayBay.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace HeThongBanVeMayBay.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public ActionResult Index()
        {
            HANHKHACH Kh = new HANHKHACH();
            return View(Kh);
        }

        public ActionResult SendMail()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.To.Add("lequocanh.qa@gmail.com");
                mail.From = new MailAddress("lequocanh.huflit@gmail.com");
                mail.Subject = "Dai Ly Quoc Anh";
                mail.Body = "Thank you for choosing our service";
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("lequocanh.huflit@gmail.com", "sincd2000"); // Enter seders User name and password  
                    smtp.EnableSsl = true;                
                    smtp.Send(mail);
                }                        
            }                    
            return Content("Đặt vé thành công");
        }

        public List<CHUYENBAY> ChooseOneWay(int Id)
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE ID = " + Id + "").ToList())
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
                    });
                }
            }
            return rtn;
        }
        public List<CHUYENBAY> ChooseTwoWay(int Id)
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE ID = " + Id + "").ToList())
                {
                    rtn.Add(new CHUYENBAY
                    {
                        ID = item.ID,
                        IDChuyenBay = item.IDChuyenBay,
                        HangBay = item.HANGBAY1.TenHangbay,
                        IDSanBayDen = item.SANBAY1.TenSB,
                        IDSanBayDi = item.SANBAY.TenSB,
                        GiaTien = item.GiaTien * Convert.ToInt32(Session["Adult"]),
                        NgayGio = item.NgayGio.Date,
                        ThoiGianBay = item.ThoiGianBay,
                    });
                }
            }
            return rtn;
        }

        public PartialViewResult OrderOneWay(int Id)
        {
            return PartialView(ChooseOneWay(Id));
        }

        public PartialViewResult OrderTwoWay(int Id)
        {
            return PartialView(ChooseTwoWay(Id));
        }

        [HttpPost]
        public ActionResult AddCus(HANHKHACH Kh)
        {
            RandomGenerator generatorcd = new RandomGenerator();
            string randomcd = generatorcd.Generate();
            //Console.WriteLine("Start: " + start);
            //for (int cnt = 0; cnt < 10; cnt++)
            //    generator.Next();
            //string[] random = generator.GetStore();
            int dem = 0;
            for (int i = 0; i < Convert.ToInt32(Session["Adult"]); i++)
            {
                database.HANHKHACHes.Add(Kh);
                database.SaveChanges();
                int cd = Convert.ToInt32(Session["IDChieuDi"]);
                var context = new QLBANVEMAYBAYEntities();
                //string IdCd = database.CHUYENBAYs.Where(s => s.ID == cd).FirstOrDefault().IDChuyenBay;
                double GiaTienCd = database.CHUYENBAYs.Where(s => s.ID == cd).FirstOrDefault().GiaTien;
                if (dem == 0 && Convert.ToInt32(Session["Adult"]) == 1)
                {
                    context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + randomcd + "', '" + cd + "', '" + Kh.CMND + "','" + GiaTienCd + "', 'Economic', 'false')");
                    context.SaveChanges();
                    if (Convert.ToInt32(Session["IDChieuVe"]) != 0)
                    {
                        RandomGenerator generatorcv = new RandomGenerator();
                        string randomcv = generatorcv.Generate();
                        int cv = Convert.ToInt32(Session["IDChieuVe"]);
                        string IdCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().IDChuyenBay;
                        double GiaTienCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().GiaTien;
                        context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + randomcv + "', '" + cv + "', '" + Kh.CMND + "','" + GiaTienCv + "', 'Economic', 'false')");
                        context.SaveChanges();
                    }
                    break;
                }
                else
                {
                    if (Convert.ToString(Session["CMND"]) == "")
                    {
                        context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + randomcd + "', '" + cd + "', '" + Kh.CMND + "','" + GiaTienCd + "', 'Economic', 'false')");
                        context.SaveChanges();
                        if (Convert.ToInt32(Session["IDChieuVe"]) != 0)
                        {
                            RandomGenerator generatorcv = new RandomGenerator();
                            string randomcv = generatorcv.Generate();
                            int cv = Convert.ToInt32(Session["IDChieuVe"]);
                            //string IdCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().IDChuyenBay;
                            double GiaTienCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().GiaTien;
                            context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + randomcv + "', '" + cv + "', '" + Kh.CMND + "','" + GiaTienCv + "', 'Economic', 'false')");
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        string cmnd_xacnhan = Convert.ToString(Session["CMND"]);
                        string madatchocd = database.PHIEUDATCHOes.Where(s => s.CMND == cmnd_xacnhan).FirstOrDefault().IDDatCho;
                        context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + madatchocd + "', '" + cd + "', '" + Kh.CMND + "','" + GiaTienCd + "', 'Economic', 'false')");
                        context.SaveChanges();
                        if (Convert.ToInt32(Session["IDChieuVe"]) != 0)
                        {
                            int cv = Convert.ToInt32(Session["IDChieuVe"]);
                            //int IdCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().ID;
                            double GiaTienCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().GiaTien;
                            string madatchocv = database.PHIEUDATCHOes.Where(s => s.CMND == cmnd_xacnhan && s.IDChuyenBay == cv).FirstOrDefault().IDDatCho;
                            context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + madatchocv + "', '" + cv + "', '" + Kh.CMND + "','" + GiaTienCv + "', 'Economic', 'false')");
                            context.SaveChanges();
                        }
                    }
                    Session["CMND"] = Kh.CMND;
                    string cmnd = Convert.ToString(Session["CMND"]);
                    string madatcholuutam = database.PHIEUDATCHOes.Where(s => s.CMND == cmnd).FirstOrDefault().IDDatCho;
                    if (database.PHIEUDATCHOes.Count(s => s.IDDatCho == madatcholuutam) < Convert.ToInt32(Session["Adult"]))
                    {
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    break;
                }
            }
            return RedirectToAction("SendMail");
        }
    }
}