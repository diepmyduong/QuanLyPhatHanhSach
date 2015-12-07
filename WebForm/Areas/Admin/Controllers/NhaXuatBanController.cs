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
    public class NhaXuatBanController : Controller
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
            return View(models);
        }

        // GET: NhaXuatBan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request");
            }
            var model = NhaXuatBanManager.find((int)id);
            if (model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            return View(model);
        }

        // GET: NhaXuatBan/Create
        public ActionResult Create()
        {
            var model = new NhaXuatBan();
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

        // GET: NhaXuatBan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request");
            }
            var model = NhaXuatBanManager.find((int)id);
            if (model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
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
                        return RedirectToAction("Details", new { id = model.MaSoNXB });
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: NhaXuatBan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request");
            }
            var model = NhaXuatBanManager.find((int)id);
            if (model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            return View(model);
        }

        // POST: NhaXuatBan/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (id == null)
                {
                    return new HttpNotFoundResult("Bad Request");
                }
                var model = NhaXuatBanManager.find((int)id);
                if (model == null)
                {
                    return new HttpNotFoundResult("Not Found!");
                }
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
            foreach (string pro in NhaXuatBan.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
    }
}
