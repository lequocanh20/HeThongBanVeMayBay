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
        public ActionResult Index(HANHKHACH Kh)
        {
            string username = Convert.ToString(Session["IsCustomer"]);
            if (Convert.ToBoolean(Session["taikhoandadangnhap"]) == false && username != "")
            {               
                foreach (var item in database.HANHKHACHes.Where(s => s.UserName == username))
                {
                    Kh.TenHanhKhach = item.TenHanhKhach;
                    Kh.CMND = item.CMND;
                    Kh.DienThoai = item.DienThoai;
                    Kh.Email = item.Email;
                    Kh.GioiTinh = item.GioiTinh;
                    Kh.NgaySinh = item.NgaySinh;
                }
            }
            else
            {
                Kh = new HANHKHACH();
            }

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
                mail.From = new MailAddress("lequocanh.qa@gmail.com");
                mail.Subject = "Đại lý vé máy bay Anh-Tường";
                mail.Body = "Cảm ơn bạn đã chọn đại lý của chúng tôi. Mã đặt chỗ của bạn là : " + madatcho;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential("lequocanh.qa@gmail.com", "sincd2000");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return RedirectToAction("Index", "HomePage1");
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
        public ActionResult AddCus(HANHKHACH Kh, FormCollection collection)
        {

            //Console.WriteLine("Start: " + start);
            //for (int cnt = 0; cnt < 10; cnt++)
            //    generator.Next();
            //string[] random = generator.GetStore()
            var remember = collection["Remember"];
            Session["remember"] = remember;
            if (remember != null)
            {
                Session["email"] = Kh.Email;
            }
            int dem = 0;
            for (int i = 0; i < Convert.ToInt32(Session["Adult"]); i++)
            {
                var get_cmnd_from_database = from u
                                             in database.HANHKHACHes
                                             where u.CMND == Kh.CMND
                                             select new { u.CMND };
                string check_cmnd = get_cmnd_from_database.Select(a => a.CMND).FirstOrDefault();
                if (check_cmnd != null)
                {
                    database.SaveChanges();
                }
                else
                {
                    Kh.Status = "NonUser";
                    database.HANHKHACHes.Add(Kh);
                    database.SaveChanges();
                }
                int cd = Convert.ToInt32(Session["IDChieuDi"]);
                var context = new QLBANVEMAYBAYEntities();
                //string IdCd = database.CHUYENBAYs.Where(s => s.ID == cd).FirstOrDefault().IDChuyenBay;
                double GiaTienCd = database.CHUYENBAYs.Where(s => s.ID == cd).FirstOrDefault().GiaTien;
                if (dem == 0 && Convert.ToInt32(Session["Adult"]) == 1)
                {
                    Session["email"] = Kh.Email;
                    RandomGenerator generatorcd = new RandomGenerator();
                    string randomcd = generatorcd.Generate();
                    context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + randomcd + "', '" + cd + "', '" + Kh.CMND + "','" + GiaTienCd + "', 'Economic', 'UnActive')");
                    context.Database.ExecuteSqlCommand("UPDATE CHUYENBAY SET SoGheHang2 = SoGheHang2 - 1 WHERE ID = " + database.PHIEUDATCHOes.Where(s => s.IDDatCho == randomcd).FirstOrDefault().IDChuyenBay + "");
                    context.SaveChanges();
                    if (Convert.ToString(Session["CMND"]) == "")
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
                        context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + randomcv + "', '" + cv + "', '" + Kh.CMND + "','" + GiaTienCv + "', 'Economic', 'UnActive')");
                        context.Database.ExecuteSqlCommand("UPDATE CHUYENBAY SET SoGheHang2 = SoGheHang2 - 1 WHERE ID = " + database.PHIEUDATCHOes.Where(s => s.IDDatCho == randomcv).FirstOrDefault().IDChuyenBay + "");
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
                        context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + randomcd + "', '" + cd + "', '" + Kh.CMND + "','" + GiaTienCd + "', 'Economic', 'UnActive')");
                        context.Database.ExecuteSqlCommand("UPDATE CHUYENBAY SET SoGheHang2 = SoGheHang2 - 1 WHERE ID = " + database.PHIEUDATCHOes.Where(s => s.IDDatCho == randomcd).FirstOrDefault().IDChuyenBay + "");
                        context.SaveChanges();
                        if (Convert.ToInt32(Session["IDChieuVe"]) != 0)
                        {
                            RandomGenerator generatorcv = new RandomGenerator();
                            string randomcv = generatorcv.Generate();
                            int cv = Convert.ToInt32(Session["IDChieuVe"]);
                            //string IdCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().IDChuyenBay;
                            double GiaTienCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().GiaTien;
                            context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + randomcv + "', '" + cv + "', '" + Kh.CMND + "','" + GiaTienCv + "', 'Economic', 'UnActive')");
                            context.Database.ExecuteSqlCommand("UPDATE CHUYENBAY SET SoGheHang2 = SoGheHang2 - 1 WHERE ID = " + database.PHIEUDATCHOes.Where(s => s.IDDatCho == randomcv).FirstOrDefault().IDChuyenBay + "");
                            context.SaveChanges();
                        }
                        if (Convert.ToString(Session["IsCustomer"]) != "")
                        {
                            Session["taikhoandadangnhap"] = true;
                        }
                    }
                    else
                    {
                        string cmnd_xacnhan = Convert.ToString(Session["CMND"]);
                        string madatchocd = database.PHIEUDATCHOes.Where(s => s.CMND == cmnd_xacnhan).FirstOrDefault().IDDatCho;
                        context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + madatchocd + "', '" + cd + "', '" + Kh.CMND + "','" + GiaTienCd + "', 'Economic', 'UnActive')");
                        context.Database.ExecuteSqlCommand("UPDATE CHUYENBAY SET SoGheHang2 = SoGheHang2 - 1 WHERE ID = " + database.PHIEUDATCHOes.Where(s => s.IDDatCho == madatchocd).FirstOrDefault().IDChuyenBay + "");
                        context.SaveChanges();
                        if (Convert.ToInt32(Session["IDChieuVe"]) != 0)
                        {
                            int cv = Convert.ToInt32(Session["IDChieuVe"]);
                            //int IdCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().ID;
                            double GiaTienCv = database.CHUYENBAYs.Where(s => s.ID == cv).FirstOrDefault().GiaTien;
                            string madatchocv = database.PHIEUDATCHOes.Where(s => s.CMND == cmnd_xacnhan && s.IDChuyenBay == cv).FirstOrDefault().IDDatCho;
                            context.Database.ExecuteSqlCommand("INSERT INTO PHIEUDATCHO VALUES ('" + madatchocv + "', '" + cv + "', '" + Kh.CMND + "','" + GiaTienCv + "', 'Economic', 'UnActive')");
                            context.Database.ExecuteSqlCommand("UPDATE CHUYENBAY SET SoGheHang2 = SoGheHang2 - 1 WHERE ID = " + database.PHIEUDATCHOes.Where(s => s.IDDatCho == madatchocv).FirstOrDefault().IDChuyenBay + "");
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
                    if (Convert.ToString(Session["remember"]) == "")
                    {
                        Session["email"] = Kh.Email;
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
                context.Database.ExecuteSqlCommand("INSERT INTO VECHUYENBAY(IDVeChuyenBay, IDChuyenBay, CMND, GiaTien, LoaiVe, Status) SELECT IDDatCho, IDChuyenBay, CMND, GiaTien, LoaiVe, TrangThai FROM PHIEUDATCHO WHERE TrangThai = 'Active'");
                context.Database.ExecuteSqlCommand("UPDATE PHIEUDATCHO SET TrangThai = 'Active' WHERE IDDatCho = '" + madatcho + "'");
                if (madatcho == database.VECHUYENBAYs.Where(s => s.IDVeChuyenBay == madatcho).FirstOrDefault().IDVeChuyenBay)
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM PHIEUDATCHO WHERE IDDatCho = '" + madatcho + "'");
                }
                return RedirectToAction("SendMail");
            }
            else
            {
                return Content("Thanh toán bị lỗi");
            }
        }

        [Authorize(Roles = "true, false")]
        public ActionResult GetCustomer()
        {
            return View(database.HANHKHACHes.ToList());
        }

        public ActionResult Block(string cmnd)
        {
            using (var context = new QLBANVEMAYBAYEntities())
            {
                if (database.HANHKHACHes.Where(s => s.CMND == cmnd).FirstOrDefault().Status == "Active")
                {
                    context.Database.ExecuteSqlCommand("UPDATE HANHKHACH SET Status = 'UnActive' WHERE CMND = '" + cmnd + "'");
                }
                else
                {
                    context.Database.ExecuteSqlCommand("UPDATE HANHKHACH SET Status = 'Active' WHERE CMND = '" + cmnd + "'");
                }
            }
            return RedirectToAction("GetCustomer", "Customer");
        }

        public ActionResult Edit(string username)
        {
            username = Convert.ToString(Session["IsCustomer"]);
            return View(database.HANHKHACHes.Where(s => s.UserName == username).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(string username, HANHKHACH kh)
        {
            username = Convert.ToString(Session["IsCustomer"]);
            database.Entry(kh).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Edit", "Customer", username);
        }

        public ActionResult Register()
        {
            HANHKHACH kh = new HANHKHACH();
            return View(kh);
        }

        [HttpPost]
        public ActionResult Register(HANHKHACH kh)
        {
            try
            {
                kh.ChucVu = "KH";
                kh.Status = "Active";
                database.HANHKHACHes.Add(kh);
                database.SaveChanges();
                return RedirectToAction("LoginHK", "Authentication");
            }
            catch
            {
                return View("Register");
            }
        }

        public List<LICHSUDATVE> History(string username)
        {
            var rtn = new List<LICHSUDATVE>();
            var his = from u in database.VECHUYENBAYs.Where(s => s.CMND == database.HANHKHACHes.Where(t => t.UserName == username).FirstOrDefault().CMND)
                       join c in database.CHUYENBAYs on u.IDChuyenBay equals c.ID
                       select new { HangBay = c.HANGBAY1.TenHangbay, IDSanBayDi = c.IDSanBayDi, IDSanBayDen = c.IDSanBayDen, NgayBay = c.NgayBay, GioBay = c.GioBay, GiaTien = u.GiaTien, Status = u.Status };
            foreach (var item in his.ToList())
            {
                rtn.Add(new LICHSUDATVE
                {
                    HangBay = item.HangBay,
                    IDSanBayDi = item.IDSanBayDi,
                    IDSanBayDen = item.IDSanBayDen,
                    NgayBay = item.NgayBay,
                    GioBay = item.GioBay,
                    GiaTien = item.GiaTien,
                    Status = item.Status
                });
            }
            return rtn;
        }
        public PartialViewResult RenderHistory(string username)
        {
            return PartialView(History(username));
        }
    }
}