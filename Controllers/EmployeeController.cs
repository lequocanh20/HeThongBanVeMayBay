using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public static List<NHANVIEN> SelectAllArticle()
        {
            var rtn = new List<NHANVIEN>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.NHANVIENs)
                {
                    rtn.Add(new NHANVIEN
                    {
                        ID = item.ID,
                        IDNhanVien = item.IDNhanVien,
                        TenNV = item.TenNV,
                        NgaySinh = item.NgaySinh,
                        NgayVaoLam = item.NgayVaoLam,
                        NgayNghiLam = item.NgayNghiLam,
                        ChucVu = item.ChucVu,
                        BoPhan = item.CATEGORY.NameCate,
                        ImageEmp = item.ImageEmp
                    });
                }
            }
            return rtn;
        }
        public static List<CATEGORY> SelectAllArticle1()
        {
            var rtn = new List<CATEGORY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CATEGORies)
                {
                    rtn.Add(new CATEGORY
                    {
                        ID = item.ID,
                        IDCate = item.IDCate,
                        NameCate = item.NameCate
                    });
                }
            }
            return rtn;
        }
        public ActionResult Index()
        {
            var lsNV = SelectAllArticle().ToList();
            return View(lsNV);
        }
        public ActionResult Create()
        {
            List<CATEGORY> list = SelectAllArticle1().ToList();
            ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", "");
            NHANVIEN nv = new NHANVIEN();
            return View(nv);
        }
        [HttpPost]
        public ActionResult Create(NHANVIEN nv)
        {
            List<CATEGORY> list = database.CATEGORies.ToList();
            try
            {
                if (nv.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(nv.UploadImage.FileName);
                    string extent = Path.GetExtension(nv.UploadImage.FileName);
                    filename = filename + extent;
                    nv.ImageEmp = "~/Content/images/" + filename;
                    nv.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", 1);
                database.NHANVIENs.Add(nv);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int Id)
        {
            List<CATEGORY> list = database.CATEGORies.ToList();
            ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", "");
            return View(database.NHANVIENs.Where(s => s.ID == Id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int Id, NHANVIEN nv)
        {
            List<CATEGORY> list = database.CATEGORies.ToList();
            database.Entry(nv).State = System.Data.Entity.EntityState.Modified;
            try
            {
                if (nv.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(nv.UploadImage.FileName);
                    string extent = Path.GetExtension(nv.UploadImage.FileName);
                    filename = filename + extent;
                    nv.ImageEmp = "~/Content/images/" + filename;
                    nv.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", 1);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete (int Id)
        {
            return View(database.NHANVIENs.Where(s => s.ID == Id).FirstOrDefault());
        }
        
        [HttpPost]
        public ActionResult Delete (int Id, NHANVIEN nv)
        {
            try
            {
                nv = database.NHANVIENs.Where(s => s.ID == Id).FirstOrDefault();
                database.NHANVIENs.Remove(nv);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Employee");
            }
        }
        public ActionResult Details (int Id)
        {
            return View(database.NHANVIENs.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult SelectCate()
        {
            CATEGORY se_cate = new CATEGORY();
            se_cate.ListCate = database.CATEGORies.ToList<CATEGORY>();
            return PartialView(se_cate);
        }
    }
}