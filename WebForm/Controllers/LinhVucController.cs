using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using Core.DAL;
using PagedList;

namespace WebForm.Controllers
{
    public class LinhVucController : Controller
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
            return View(models);
        }

        // GET: LinhVuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request");
            }
            var model = LinhVucManager.find((int)id);
            if (model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            return View(model);
        }

        // GET: LinhVuc/Create
        public ActionResult Create()
        {
            var model = new LinhVuc();
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
                        RedirectToAction("Details", new { id = result });
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: LinhVuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request");
            }
            var model = LinhVucManager.find((int)id);
            if (model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
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
                        return RedirectToAction("Details", new { id = model.MaSoLinhVuc });
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: LinhVuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpNotFoundResult("Bad Request");
            }
            var model = LinhVucManager.find((int)id);
            if(model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            return View(model);
        }

        // POST: LinhVuc/Delete/5
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
                var model = LinhVucManager.find((int)id);
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
            foreach (string pro in LinhVuc.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
