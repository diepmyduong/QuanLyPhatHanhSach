using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using PagedList;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Data.Linq;

namespace WebForm.Controllers
{
    public class SachController : Controller
    {
        private CultureInfo _cultureInfo;
        private List<string> _propertiesName;

        public CultureInfo CultureInfo
        {
            get
            {
                if(_cultureInfo == null)
                {
                    _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
                }
                return _cultureInfo;
            }
        }
        public List<string> PropertiesName
        {
            get
            {
                if (_propertiesName == null)
                {
                    var pros = typeof(SachManager.Properties).GetFields();
                    _propertiesName = pros.Select(p => p.Name).ToList();
                }
                return _propertiesName;
            }
        }
        // GET: Sach
        public ActionResult Index(int page = 1, int pageSize = 10,string search = null)
        {
            List<Sach> DMSach = null ;
            ViewBag.cultureInfo = CultureInfo;
            if (!String.IsNullOrEmpty(search))
            {
                DMSach = SachManager.filter(search);
                ViewBag.SearchKey = search;
            }
            else
            {
                DMSach = SachManager.getAll();
            }
            
            var models = DMSach.ToPagedList(page, pageSize);
            return View(models);
        }

        // GET: Sach/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.cultureInfo = CultureInfo;
            var model = SachManager.find(id);
            if (model.Anh == null)
            {
                ViewBag.DefaultImage = "/Resources/DefaultImage.png";
            }
            else
            {
                var base64 = Convert.ToBase64String(ImagesHelper.ImageToBinary(model.Image));
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.imgSrc = imgSrc;
            }
            return View(model);
        }

        // GET: Sach/Create
        public ActionResult Create()
        {
            var model = new Sach();
            ViewBag.DMNXB = new SelectList(NhaXuatBanManager.getAll(),
                nameof(NhaXuatBanManager.Properties.MaSoNXB),
                nameof(NhaXuatBanManager.Properties.TenNXB),"");
            ViewBag.DMLinhVuc = new SelectList(LinhVucManager.getAll(),
                nameof(LinhVucManager.Properties.MaSoLinhVuc),
                nameof(LinhVucManager.Properties.TenLinhVuc), "");
            return View(model);
        }

        // POST: Sach/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sach/Edit/5
        public ActionResult Edit(int id)
        {
            var model = SachManager.find(id);
            ViewBag.DMNXB = new SelectList(NhaXuatBanManager.getAll(),
                nameof(NhaXuatBanManager.Properties.MaSoNXB),
                nameof(NhaXuatBanManager.Properties.TenNXB), "");
            ViewBag.DMLinhVuc = new SelectList(LinhVucManager.getAll(),
                nameof(LinhVucManager.Properties.MaSoLinhVuc),
                nameof(LinhVucManager.Properties.TenLinhVuc), "");
            if(model.Image != null)
            {
                var base64 = Convert.ToBase64String(ImagesHelper.ImageToBinary(model.Image));
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.imgSrc = imgSrc;
            }
            return View(model);
        }

        // POST: Sach/Edit/5
        [HttpPost]
        public ActionResult Edit(Sach model,HttpPostedFileBase file, int id)
        {
            try
            {
                if (file != null)
                {
                    model.Image = Image.FromStream(file.InputStream);
                    ////var a = model.ImageFolderPath();
                    //var fileName = id + Path.GetExtension(file.FileName);
                    //var path = Path.GetFullPath(model.ImageFolderPath()) + fileName;
                    //file.SaveAs(path);
                    //model.HinhAnh = fileName;
                    //if(model.MaSoSach == 0)
                    //{
                    //    return View();
                    //}
                }
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (SachManager.edit(model))
                    {
                        return RedirectToAction("Details", new { id = model.MaSoSach });
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Sach/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sach/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetProperties(string request)
        {
            List<string> results = new List<string>();
            foreach(string pro in PropertiesName)
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
