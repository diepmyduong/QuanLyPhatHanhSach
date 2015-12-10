using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using Core.DAL;
using PagedList;

namespace WebForm.Areas.Admin.Controllers
{
    public class LinhVucController : BaseController
    {

        #region Private Properties
        #endregion

        #region Public Properties
        #endregion

        #region Actions
        // GET: LinhVuc
        public ActionResult Index(int page = 1, int pageSize = 10, string search = null)
        {
            List<LinhVuc> DMLinhVuc = null;
            if (!String.IsNullOrEmpty(search))
            {
                DMLinhVuc = LinhVucManager.filter(search);
                ViewBag.SearchKey = search;
            }
            else
            {
                DMLinhVuc = LinhVucManager.getAllALive();
            }
            var models = DMLinhVuc.ToPagedList(page, pageSize);
            setAlertMessage();
            return View(models);
        }

        // GET: LinhVuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = LinhVucManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // GET: LinhVuc/Create
        public ActionResult Create()
        {
            var model = new LinhVuc();
            setAlertMessage();
            return View(model);
        }

        // POST: LinhVuc/Create
        [HttpPost]
        public ActionResult Create(LinhVuc model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = LinhVucManager.add(model);
                    if (result != 0)
                    {
                        putSuccessMessage("Thêm thành công");
                        RedirectToAction("Details", new { id = result });
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
                setAlertMessage();
                return View(model);
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Create");
            }
        }

        // GET: LinhVuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = LinhVucManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // POST: LinhVuc/Edit/5
        [HttpPost]
        public ActionResult Edit(LinhVuc model, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (LinhVucManager.edit(model))
                    {
                        putSuccessMessage("Cập nhật thành công");
                        return RedirectToAction("Details", new { id = model.MaSoLinhVuc });
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
                return RedirectToAction("Edit", new { id = model.MaSoLinhVuc });
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Edit", new { id = model.MaSoLinhVuc });
            }
        }

        // GET: LinhVuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = LinhVucManager.find((int)id);
            if(model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // POST: LinhVuc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var model = LinhVucManager.find((int)id);
                if (model == null)
                {
                    putErrorMessage("Không tìm thấy");
                    return RedirectToAction("Index");
                }
                if (model.delete())
                {
                    putSuccessMessage("Xóa thành công");
                    return RedirectToAction("Index");
                }
                else
                {
                    putErrorMessage("Xóa không thành công");
                }
                return RedirectToAction("Delete",new { id });
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
            foreach (string pro in LinhVuc.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
