using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using PagedList;
using System.Globalization;

namespace WebForm.Controllers
{
    public class DaiLyController : Controller
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
        // GET: DaiLy
        public ActionResult Index(int page = 1, int pageSize = 10, string search = null)
        {
            List<DaiLy> DMDaiLy = null;
            if (!String.IsNullOrEmpty(search))
            {
                DMDaiLy = DaiLyManager.filter(search);
                ViewBag.SearchKey = search;
            }
            else
            {
                DMDaiLy = DaiLyManager.getAllAlive();
            }
            var models = DMDaiLy.ToPagedList(page, pageSize);
            return View(models);
        }

        // GET: DaiLy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            var model = DaiLyManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            return View(model);
        }

        // GET: DaiLy/Create
        public ActionResult Create()
        {
            var model = new DaiLy();
            return View(model);
        }

        // POST: DaiLy/Create
        [HttpPost]
        public ActionResult Create(DaiLy model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = DaiLyManager.add(model);
                    if (result != 0)
                    {
                        return RedirectToAction("Details", new { id = result });
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: DaiLy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            var model = DaiLyManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            return View(model);
        }

        // POST: DaiLy/Edit/5
        [HttpPost]
        public ActionResult Edit(DaiLy model, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (DaiLyManager.edit(model))
                    {
                        return RedirectToAction("Details", new { id = model.MaSoDaiLy });
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: DaiLy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            var model = DaiLyManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            return View(model);

        }

        // POST: DaiLy/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                if (id == null)
                {
                    return new HttpNotFoundResult("Bad Request!");
                }
                var model = DaiLyManager.find((int)id);
                if (model == null || model.TrangThai == 0)
                {
                    return new HttpNotFoundResult("Not Found!");
                }
                // TODO: Add delete logic here
                if (model.delete())
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region JSON REQUEST
        public JsonResult GetProperties(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in DaiLy.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
    }
}
