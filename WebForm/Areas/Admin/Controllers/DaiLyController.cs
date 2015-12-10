using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using PagedList;
using System.Globalization;

namespace WebForm.Areas.Admin.Controllers
{
    public class DaiLyController : BaseController
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
            setAlertMessage();
            return View(models);
        }

        // GET: DaiLy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = DaiLyManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy đại lý");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // GET: DaiLy/Create
        public ActionResult Create()
        {
            var model = new DaiLy();
            setAlertMessage();
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
                        putSuccessMessage("Thâm thành công");
                        return RedirectToAction("Details", new { id = result });
                    }
                    else
                    {
                        putErrorMessage("Thêm thất bại");
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                }
                return View(model);
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Create");
            }
        }

        // GET: DaiLy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = DaiLyManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy đại lý");
                return RedirectToAction("Index");
            }
            setAlertMessage();
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
                        putSuccessMessage("Cập nhật thành công");
                        return RedirectToAction("Details", new { id = model.MaSoDaiLy });
                    }
                    else
                    {
                        putErrorMessage("Cập nhật thất bại");
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                }
                return View(model);
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Edit", new { id = model.MaSoDaiLy });
            }
        }

        // GET: DaiLy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = DaiLyManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy đại lý");
                return RedirectToAction("Index");
            }
            setAlertMessage();
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
                    putErrorMessage("Đường dẫn không chính xác");
                    return RedirectToAction("Index");
                }
                var model = DaiLyManager.find((int)id);
                if (model == null || model.TrangThai == 0)
                {
                    putErrorMessage("Không tìm thấy đại lý");
                    return RedirectToAction("Index");
                }
                // TODO: Add delete logic here
                if (model.delete())
                {
                    putSuccessMessage("Đã xóa");
                    return RedirectToAction("Index");
                }
                else
                {
                    putErrorMessage("Xóa thất bại");
                    return RedirectToAction("Delete", new { id });
                }
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Delete", new { id });
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
