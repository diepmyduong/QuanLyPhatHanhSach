﻿using System;
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
        #region Private Properties
        private CultureInfo _cultureInfo; // Thông tin văn hóa
        #endregion

        #region Public Properties
        public CultureInfo CultureInfo
        {
            get
            {
                if (_cultureInfo == null)
                {
                    _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
                }
                return _cultureInfo;
            }
        }
        #endregion

        #region Actions
        // GET: Sach
        public ActionResult Index(int page = 1, int pageSize = 10, string search = null)
        {
            List<Sach> DMSach = null;
            ViewBag.cultureInfo = CultureInfo;
            if (!String.IsNullOrEmpty(search))
            {
                DMSach = SachManager.filter(search);
                ViewBag.SearchKey = search;
            }
            else
            {
                DMSach = SachManager.getAllAlive();
            }

            var models = DMSach.ToPagedList(page, pageSize);
            return View(models);
        }

        // GET: Sach/Details/5
        public ActionResult Details(int? id) // id là mã số sách
        {
            if(id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            ViewBag.cultureInfo = CultureInfo; // Sử dụng cho hiển thị tiền tệ VNĐ
            var model = SachManager.find((int)id);
            if(model == null || model.TrangThai == 0)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            if (model.HinhAnh == null)
            {
                ViewBag.DefaultImage = "/Resources/DefaultImage.png"; // Load hình ảnh mặc định nếu chưa có hình
            }
            else
            {
                ViewBag.imgSrc = ImagesHelper.ImageToDataBase64String(model.HinhAnhTypeImage);
            }
            return View(model);
        }

        // GET: Sach/Create
        public ActionResult Create()
        {
            var model = new Sach();
            //Combobox nhà xuất bản
            ViewBag.DMNXB = new SelectList(NhaXuatBanManager.getAllAlive(),
                nameof(NhaXuatBanManager.Properties.MaSoNXB),
                nameof(NhaXuatBanManager.Properties.TenNXB), "");
            //Combobox lĩnh vực
            ViewBag.DMLinhVuc = new SelectList(LinhVucManager.getAllALive(),
                nameof(LinhVucManager.Properties.MaSoLinhVuc),
                nameof(LinhVucManager.Properties.TenLinhVuc), "");
            return View(model);
        }

        // POST: Sach/Create
        [HttpPost]
        public ActionResult Create(Sach model, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        model.HinhAnhTypeImage = Image.FromStream(file.InputStream);
                    }
                    var result = SachManager.add(model);
                    if (result != 0)
                    {
                        return RedirectToAction("Details", new { id = result });
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Sach/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            var model = SachManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            //Combobox Nhà xuất bản
            ViewBag.DMNXB = new SelectList(NhaXuatBanManager.getAllAlive(),
                nameof(NhaXuatBanManager.Properties.MaSoNXB),
                nameof(NhaXuatBanManager.Properties.TenNXB), "");
            //Combobox lĩnh vực
            ViewBag.DMLinhVuc = new SelectList(LinhVucManager.getAllALive(),
                nameof(LinhVucManager.Properties.MaSoLinhVuc),
                nameof(LinhVucManager.Properties.TenLinhVuc), "");
            if (model.HinhAnh != null)
            {
                ViewBag.imgSrc = ImagesHelper.ImageToDataBase64String(model.HinhAnhTypeImage);
            }
            return View(model);
        }

        // POST: Sach/Edit/5
        [HttpPost]
        public ActionResult Edit(Sach model, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        model.HinhAnhTypeImage = Image.FromStream(file.InputStream);
                    }
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
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            var model = SachManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            ViewBag.cultureInfo = CultureInfo; // Sử dụng cho hiển thị tiền tệ VNĐ
            if (model.HinhAnh == null)
            {
                ViewBag.DefaultImage = "/Resources/DefaultImage.png"; // Load hình ảnh mặc định nếu chưa có hình
            }
            else
            {
                ViewBag.imgSrc = ImagesHelper.ImageToDataBase64String(model.HinhAnhTypeImage);
            }
            return View(model);
        }

        // POST: Sach/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                if(id == null)
                {
                    return new HttpNotFoundResult("Bad Request");
                }
                var model = SachManager.find((int)id);
                if(model == null || model.TrangThai == 0)
                {
                    return new HttpNotFoundResult("Not Found!");
                }
                // TODO: Add delete logic here
                if (model.delete())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(id);
                }
            }
            catch
            {
                return View(id);
            }
        }
        #endregion

        #region JSON REQUEST
        public JsonResult GetProperties(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in Sach.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
