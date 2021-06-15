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
            string cmnd = Convert.ToString(Session["CMND"]);
            string madatcho = database.VECHUYENBAYs.Where(s => s.CMND == cmnd).OrderByDescending(x => x.ID).FirstOrDefault().IDVeChuyenBay;
            Session.Remove("CMND");
            using (MailMessage mail = new MailMessage())
            {
                mail.To.Add(Convert.ToString(Session["email"]));
                Session.Remove("email");
                mail.From = new MailAddress("lequocanh.huflit@gmail.com");
                mail.Subject = "Đại lý vé máy bay Anh-Tường";
                mail.Body = "Cảm ơn bạn đã chọn đại lý của chúng tôi. Mã đặt chỗ của bạn là : " + madatcho;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential("lequocanh.huflit@gmail.com", "sincd2000");
                    smtp.EnableSsl = true;                
                    smtp.Send(mail);
                }                        
            }                    
            Content("Đặt vé thành công");
            return RedirectToAction("Index", "BookTicket");
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
                        NgayBay = item.NgayBay.Date,
                        GioBay = item.GioBay,
                        ThoiGianToiDuKien = item.ThoiGianToiDuKien,
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
                        NgayBay = item.NgayBay.Date,
                        GioBay = item.GioBay,
                        ThoiGianToiDuKien = item.ThoiGianToiDuKien,
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
            
            //Console.WriteLine("Start: " + start);
            //for (int cnt = 0; cnt < 10; cnt++)
            //    generator.Next();
            //string[] random = generator.GetStore();
            int dem = 0;
            for (int i = 0; i < Convert.ToInt32(Session["Adult"]); i++)
            {
                database.HANHKHACHes.Add(Kh);
                var get_database = from u in database.HANHKHACHes where u.CMND == Kh.CMND select new { u.CMND };
                string check_cmnd = get_database.Select(a => a.CMND).FirstOrDefault();
                if (check_cmnd != null)
                {
                    Session["CMND"] = Kh.CMND;
                }    
                else
                {
                    database.SaveChanges();
                }                   
                Session["email"] = Kh.Email;
                int cd = Convert.ToInt32(Session["IDChieuDi"]);
                var context = new QLBANVEMAYBAYEntities();
                //string IdCd = database.CHUYENBAYs.Where(s => s.ID == cd).FirstOrDefault().IDChuyenBay;
                double GiaTienCd = database.CHUYENBAYs.Where(s => s.ID == cd).FirstOrDefault().GiaTien;
                if (dem == 0 && Convert.ToInt32(Session["Adult"]) == 1)
                {
                    RandomGenerator generatorcd = new RandomGenerator();
                    string randomcd = generatorcd.Generate();
                    context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + randomcd + "', '" + cd + "', '" + Kh.CMND + "','" + GiaTienCd + "', 'Economic', 'false')");
                    context.SaveChanges();
                    if(Convert.ToString(Session["CMND"]) == "")
                    {
                        Session["CMND"] = Kh.CMND;
                        Session.Remove("IDChieuDi");
                    }    
                    else
                    {
                        Session.Remove("IDChieuDi");
                    }
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
                        RandomGenerator generatorcd = new RandomGenerator();
                        string randomcd = generatorcd.Generate();
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
                    Session.Remove("IDChieuDi");
                    Session.Remove("IDChieuVe");
                    break;                   
                }
            }
            return RedirectToAction("Payment");
        }
        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(string cardname, string cardnumber, string expmonth, string expyear, string cvv)
        {
            if (cardname == "LE QUOC ANH" && cardnumber == "1234-4567-7890-0123" && expmonth == "06" && expyear == "2021" && cvv == "123")
            {
                var context = new QLBANVEMAYBAYEntities();
                string cmnd = Convert.ToString(Session["CMND"]);
                string madatcho = database.PHIEUDATCHOes.Where(s => s.CMND == cmnd).OrderBy(x => x.TrangThai).FirstOrDefault().IDDatCho;
                context.Database.ExecuteSqlCommand("UPDATE PHIEUDATCHO SET TrangThai = 'true' WHERE IDDatCho = '" + madatcho + "'");
                context.Database.ExecuteSqlCommand("INSERT INTO VECHUYENBAY(IDVeChuyenBay, IDChuyenBay, CMND, GiaTien, LoaiVe) SELECT IDDatCho, IDChuyenBay, CMND, GiaTien, LoaiVe FROM PHIEUDATCHO WHERE TrangThai = 'true'");
                if (madatcho == database.VECHUYENBAYs.Where(s => s.IDVeChuyenBay == madatcho).FirstOrDefault().IDVeChuyenBay)
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM PHIEUDATCHO WHERE IDDatCho = '"+ madatcho +"'");
                }
                return RedirectToAction("SendMail");
            }    
            else
            {
                return Content("Thanh toán bị lỗi");
            }    
        }
    }
}