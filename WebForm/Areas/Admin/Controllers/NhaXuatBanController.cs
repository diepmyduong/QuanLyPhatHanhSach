using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using PagedList;

namespace WebForm.Areas.Admin.Controllers
{
    public class NhaXuatBanController : BaseController
    {
        #region Private Properties
        #endregion

        #region Public Properties
        #endregion

        #region Actions
        // GET: NhaXuatBan
        public ActionResult Index(int page = 1, int pageSize = 10, string search = null)
        {
            List<NhaXuatBan> DMNXB = null;
            if (!String.IsNullOrEmpty(search))
            {
                DMNXB = NhaXuatBanManager.filter(search);
                ViewBag.searchKey = search;
            }
            else
            {
                DMNXB = NhaXuatBanManager.getAllAlive();
            }
            var models = DMNXB.ToPagedList(page, pageSize);
            setAlertMessage();
            return View(models);
        }

        // GET: NhaXuatBan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = NhaXuatBanManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // GET: NhaXuatBan/Create
        public ActionResult Create()
        {
            var model = new NhaXuatBan();
            setAlertMessage();
            return View(model);
        }

        // POST: NhaXuatBan/Create
        [HttpPost]
        public ActionResult Create(NhaXuatBan model, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = NhaXuatBanManager.add(model);
                    if (result != 0)
                    {
                        putSuccessMessage("Thêm thành công");
                        return RedirectToAction("Details", new { id = result });
                    }
                    else
                    {
                        putErrorMessage("Thêm không thành công");
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

        // GET: NhaXuatBan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = NhaXuatBanManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // POST: NhaXuatBan/Edit/5
        [HttpPost]
        public ActionResult Edit(NhaXuatBan model, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (NhaXuatBanManager.edit(model))
                    {
                        putSuccessMessage("Cập nhật thành công");
                        return RedirectToAction("Details", new { id = model.MaSoNXB });
                    }
                    else
                    {
                        putErrorMessage("Cập nhật không thành công");
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                }
                return RedirectToAction("Edit", new { id = model.MaSoNXB });
            }
            catch (Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Edit", new { id = model.MaSoNXB });
            }
        }

        // GET: NhaXuatBan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = NhaXuatBanManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // POST: NhaXuatBan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var model = NhaXuatBanManager.find((int)id);
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
            foreach (string pro in NhaXuatBan.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
    }
}
